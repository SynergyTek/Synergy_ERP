using System.Threading.Tasks;
using ERP.PayrollService.ViewModels;
using System.Collections.Generic;
using ERP.PayrollService.ViewModels;

namespace ERP.PayrollService.Interfaces
{
    public interface IPayrollReportService
    {
        Task<IEnumerable<PayslipViewModel>> GetPayslipsReportAsync(string periodStart, string periodEnd);
        Task<decimal> GetTotalPayrollAsync(string periodStart, string periodEnd);
    }
} 