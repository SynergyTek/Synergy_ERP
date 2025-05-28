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

        public PayslipService(
            IPayslipRepository payslipRepo,
            IEmployeeRepository employeeRepo,
            IPayrollStructureRepository structureRepo,
            IAllowanceRepository allowanceRepo,
            IDeductionRepository deductionRepo,
            IPayrollRuleRepository ruleRepo,
            IPayrollTaxConfigRepository taxConfigRepo)
        {
            _payslipRepo = payslipRepo;
            _employeeRepo = employeeRepo;
            _structureRepo = structureRepo;
            _allowanceRepo = allowanceRepo;
            _deductionRepo = deductionRepo;
            _ruleRepo = ruleRepo;
            _taxConfigRepo = taxConfigRepo;
        }

        public async Task<PayslipViewModel> GeneratePayslipAsync(int employeeId, int structureId, string periodStart, string periodEnd)
        {
            var employee = await _employeeRepo.GetByIdAsync(employeeId);
            var structure = await _structureRepo.GetByIdAsync(structureId);
            if (employee == null || structure == null) return null;

            // Get rules, allowances, deductions for the structure
            var rules = (await _ruleRepo.GetAllAsync()).Where(r => r.PayrollStructureId == structureId);
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

            decimal netPay = grossPay - totalDeductions - tax + formulaTotal;

            var payslip = new Payslip
            {
                EmployeeId = employeeId,
                PayrollStructureId = structureId,
                PeriodStart = DateTime.Parse(periodStart),
                PeriodEnd = DateTime.Parse(periodEnd),
                GrossPay = grossPay,
                NetPay = netPay
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
    }
} 