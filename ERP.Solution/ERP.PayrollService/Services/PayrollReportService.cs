using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.PayrollService.ViewModels;
using ERP.PayrollService.Interfaces;

namespace ERP.PayrollService.Services
{
    public class PayrollReportService : IPayrollReportService
    {
        private readonly IPayslipRepository _payslipRepo;

        public PayrollReportService(IPayslipRepository payslipRepo)
        {
            _payslipRepo = payslipRepo;
        }

        public async Task<IEnumerable<PayslipViewModel>> GetPayslipsReportAsync(string periodStart, string periodEnd)
        {
            var payslips = await _payslipRepo.GetAllAsync();
            var start = DateTime.Parse(periodStart);
            var end = DateTime.Parse(periodEnd);
            return payslips.Where(p => p.PeriodStart >= start && p.PeriodEnd <= end)
                .Select(p => new PayslipViewModel
                {
                    Id = p.Id,
                    PeriodStart = p.PeriodStart,
                    PeriodEnd = p.PeriodEnd,
                    GrossPay = p.GrossPay,
                    NetPay = p.NetPay
                });
        }

        public async Task<decimal> GetTotalPayrollAsync(string periodStart, string periodEnd)
        {
            var payslips = await _payslipRepo.GetAllAsync();
            var start = DateTime.Parse(periodStart);
            var end = DateTime.Parse(periodEnd);
            return payslips.Where(p => p.PeriodStart >= start && p.PeriodEnd <= end).Sum(p => p.NetPay);
        }
    }
} 