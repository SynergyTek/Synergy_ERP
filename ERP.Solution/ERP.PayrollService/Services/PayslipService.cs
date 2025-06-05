using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.ViewModels;
using ERP.PayrollService.Interfaces;
using ERP.PayrollService.Models;
using System.Linq;
using DynamicExpresso;

namespace ERP.PayrollService.Services
{
    public class PayslipService : IPayslipService
    {
        private readonly IPayslipRepository _payslipRepo;
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IPayrollStructureRepository _structureRepo;
        private readonly IAllowanceRepository _allowanceRepo;
        private readonly IDeductionRepository _deductionRepo;
        private readonly IPayrollRuleRepository _ruleRepo;
        private readonly IPayrollTaxConfigRepository _taxConfigRepo;
        private readonly ILeaveRepository _leaveRepo;
        private readonly ILoanRepository _loanRepo;
        private readonly ICurrencyRateRepository _currencyRateRepo;
        private readonly IPayslipAdjustmentRepository _payslipAdjustmentRepo;
        private readonly IPayslipStatusChangeLogRepository _statusChangeLogRepo;

        public PayslipService(
            IPayslipRepository payslipRepo,
            IEmployeeRepository employeeRepo,
            IPayrollStructureRepository structureRepo,
            IAllowanceRepository allowanceRepo,
            IDeductionRepository deductionRepo,
            IPayrollRuleRepository ruleRepo,
            IPayrollTaxConfigRepository taxConfigRepo,
            ILeaveRepository leaveRepo,
            ILoanRepository loanRepo,
            ICurrencyRateRepository currencyRateRepo,
            IPayslipAdjustmentRepository payslipAdjustmentRepo,
            IPayslipStatusChangeLogRepository statusChangeLogRepo)
        {
            _payslipRepo = payslipRepo;
            _employeeRepo = employeeRepo;
            _structureRepo = structureRepo;
            _allowanceRepo = allowanceRepo;
            _deductionRepo = deductionRepo;
            _ruleRepo = ruleRepo;
            _taxConfigRepo = taxConfigRepo;
            _leaveRepo = leaveRepo;
            _loanRepo = loanRepo;
            _currencyRateRepo = currencyRateRepo;
            _payslipAdjustmentRepo = payslipAdjustmentRepo;
            _statusChangeLogRepo = statusChangeLogRepo;
        }

        public async Task<PayslipViewModel> GeneratePayslipAsync(int employeeId, int structureId, string periodStart, string periodEnd)
        {
            var employee = await _employeeRepo.GetByIdAsync(employeeId);
            var structure = await _structureRepo.GetByIdAsync(structureId);
            if (employee == null || structure == null) return null;

            // LOCALIZATION SUPPORT
            string employeeCountry = employee.EmployeeContracts?.OrderByDescending(c => c.StartDate).FirstOrDefault()?.Country;
            if (!string.IsNullOrEmpty(employeeCountry))
            {
                structure = await _structureRepo.GetAllAsync().ContinueWith(t => t.Result.FirstOrDefault(s => s.Id == structureId && s.Country == employeeCountry));
                if (structure == null) return null;
            }
            // Get rules, allowances, deductions for the structure, filtered by country
            var rules = (await _ruleRepo.GetAllAsync()).Where(r => r.PayrollStructureId == structureId && (string.IsNullOrEmpty(employeeCountry) || r.Country == employeeCountry));
            var allowances = (await _allowanceRepo.GetAllAsync()).Where(a => a.PayrollStructureId == structureId);
            var deductions = (await _deductionRepo.GetAllAsync()).Where(d => d.PayrollStructureId == structureId);
            var taxConfigs = (await _taxConfigRepo.GetAllAsync()).Where(t => t.IsActive);

            decimal baseSalary = rules.Where(r => r.RuleType == "BaseSalary").Sum(r => r.Value);
            decimal totalAllowances = allowances.Sum(a => a.Amount);
            decimal totalDeductions = deductions.Sum(d => d.Amount);
            decimal grossPay = baseSalary + totalAllowances;
            decimal tax = 0;
            foreach (var taxConfig in taxConfigs)
            {
                if (grossPay > taxConfig.Threshold)
                {
                    tax += grossPay * taxConfig.Rate / 100m;
                }
            }

            // Evaluate advanced formulas for rules
            var interpreter = new Interpreter();
            interpreter.SetVariable("BaseSalary", baseSalary);
            interpreter.SetVariable("Allowances", totalAllowances);
            interpreter.SetVariable("Deductions", totalDeductions);
            interpreter.SetVariable("GrossPay", grossPay);
            interpreter.SetVariable("Tax", tax);
            decimal formulaTotal = 0;
            foreach (var rule in rules)
            {
                if (!string.IsNullOrWhiteSpace(rule.Formula))
                {
                    try
                    {
                        var result = interpreter.Eval(rule.Formula);
                        if (result is decimal dec)
                            formulaTotal += dec;
                        else if (result is double dbl)
                            formulaTotal += (decimal)dbl;
                        else if (result is int i)
                            formulaTotal += i;
                    }
                    catch { /* Ignore formula errors for now */ }
                }
            }

            // LEAVE DEDUCTION
            var allLeaves = await _leaveRepo.GetAllAsync();
            var periodStartDate = DateTime.Parse(periodStart);
            var periodEndDate = DateTime.Parse(periodEnd);
            var employeeLeaves = allLeaves.Where(l => l.EmployeeId == employeeId && l.Status == "Approved" && l.StartDate <= periodEndDate && l.EndDate >= periodStartDate);
            decimal unpaidLeaveDays = employeeLeaves.Where(l => l.Type == "Unpaid").Sum(l => (decimal)(l.EndDate - l.StartDate).TotalDays + 1); // inclusive
            decimal dailySalary = baseSalary / 30m; // assuming 30 days per month
            decimal unpaidLeaveDeduction = unpaidLeaveDays * dailySalary;

            // LOAN DEDUCTION
            var allLoans = await _loanRepo.GetAllAsync();
            var activeLoans = allLoans.Where(l => l.EmployeeId == employeeId && l.Status == "Active" && l.Remaining > 0);
            decimal loanDeduction = activeLoans.Sum(l => l.Installment);

            // MULTI-CURRENCY SUPPORT
            string baseCurrency = "INR";
            string contractCurrency = employee.EmployeeContracts?.OrderByDescending(c => c.StartDate).FirstOrDefault()?.Currency ?? baseCurrency;
            decimal conversionRate = 1m;
            if (contractCurrency != baseCurrency)
            {
                var rates = await _currencyRateRepo.GetAllAsync();
                var latestRate = rates
                    .Where(r => r.FromCurrency == baseCurrency && r.ToCurrency == contractCurrency)
                    .OrderByDescending(r => r.Date)
                    .FirstOrDefault();
                if (latestRate != null)
                    conversionRate = latestRate.Rate;
            }
            // Convert all amounts if needed
            baseSalary *= conversionRate;
            totalAllowances *= conversionRate;
            totalDeductions *= conversionRate;
            grossPay *= conversionRate;
            tax *= conversionRate;
            formulaTotal *= conversionRate;
            dailySalary *= conversionRate;
            unpaidLeaveDeduction *= conversionRate;
            loanDeduction *= conversionRate;

            // Payslip Adjustments
            decimal adjustmentTotal = 0;
            var allAdjustments = await _payslipAdjustmentRepo.GetAllAsync();
            // Since payslip is not yet created, filter by employee, structure, and period
            adjustmentTotal = allAdjustments.Where(a =>
                a.Payslip != null &&
                a.Payslip.EmployeeId == employeeId &&
                a.Payslip.PayrollStructureId == structureId &&
                a.Payslip.PeriodStart == periodStartDate &&
                a.Payslip.PeriodEnd == periodEndDate
            ).Sum(a => a.Amount);
            // Add adjustments to net pay
            decimal netPay = grossPay - totalDeductions - tax + formulaTotal - unpaidLeaveDeduction - loanDeduction + adjustmentTotal;

            var payslip = new Payslip
            {
                EmployeeId = employeeId,
                PayrollStructureId = structureId,
                PeriodStart = DateTime.Parse(periodStart),
                PeriodEnd = DateTime.Parse(periodEnd),
                GrossPay = grossPay,
                NetPay = netPay,
                Currency = contractCurrency
            };
            var created = await _payslipRepo.AddAsync(payslip);
            return new PayslipViewModel
            {
                Id = created.Id,
                Employee = new EmployeeViewModel { Id = employee.Id, FirstName = employee.FirstName, LastName = employee.LastName, Email = employee.Email, Address = employee.Address, Department = employee.Department },
                PayrollStructure = new PayrollStructureViewModel { Id = structure.Id, Name = structure.Name, Description = structure.Description },
                PeriodStart = created.PeriodStart,
                PeriodEnd = created.PeriodEnd,
                GrossPay = created.GrossPay,
                NetPay = created.NetPay
            };
        }

        public async Task<IEnumerable<PayslipViewModel>> GeneratePayslipsBatchAsync(List<int> employeeIds, int structureId, string periodStart, string periodEnd)
        {
            var result = new List<PayslipViewModel>();
            foreach (var empId in employeeIds)
            {
                var payslip = await GeneratePayslipAsync(empId, structureId, periodStart, periodEnd);
                if (payslip != null)
                    result.Add(payslip);
            }
            return result;
        }

        public async Task<IEnumerable<PayslipViewModel>> GetPayslipsForEmployeeAsync(int employeeId)
        {
            var payslips = await _payslipRepo.GetAllAsync();
            var result = new List<PayslipViewModel>();
            foreach (var p in payslips)
            {
                if (p.EmployeeId == employeeId)
                {
                    result.Add(new PayslipViewModel
                    {
                        Id = p.Id,
                        PeriodStart = p.PeriodStart,
                        PeriodEnd = p.PeriodEnd,
                        GrossPay = p.GrossPay,
                        NetPay = p.NetPay
                    });
                }
            }
            return result;
        }

        public async Task<PayslipViewModel> GetPayslipByIdAsync(int payslipId)
        {
            var p = await _payslipRepo.GetByIdAsync(payslipId);
            if (p == null) return null;
            return new PayslipViewModel
            {
                Id = p.Id,
                PeriodStart = p.PeriodStart,
                PeriodEnd = p.PeriodEnd,
                GrossPay = p.GrossPay,
                NetPay = p.NetPay
            };
        }

        public async Task<bool> UpdatePayslipStatusAsync(int payslipId, string status, string reason = null)
        {
            var payslip = await _payslipRepo.GetByIdAsync(payslipId);
            if (payslip == null) return false;
            var oldStatus = payslip.Status;
            payslip.Status = status;
            await _payslipRepo.UpdateAsync(payslip);
            // Log the status change
            var log = new PayslipStatusChangeLog
            {
                PayslipId = payslipId,
                OldStatus = oldStatus,
                NewStatus = status,
                ChangeDate = DateTime.UtcNow,
                ChangedBy = System.Threading.Thread.CurrentPrincipal?.Identity?.Name ?? "system",
                Reason = reason
            };
            await _statusChangeLogRepo.AddAsync(log);
            return true;
        }
    }
} 