using System.Threading.Tasks;
using ERP.PayrollService.ViewModels;
using System.Collections.Generic;

namespace ERP.PayrollService.Interfaces
{
    public interface IPayslipService
    {
        Task<PayslipViewModel> GeneratePayslipAsync(int employeeId, int structureId, string periodStart, string periodEnd);
        Task<IEnumerable<PayslipViewModel>> GeneratePayslipsBatchAsync(List<int> employeeIds, int structureId, string periodStart, string periodEnd);
        Task<IEnumerable<PayslipViewModel>> GetPayslipsForEmployeeAsync(int employeeId);
        Task<PayslipViewModel> GetPayslipByIdAsync(int payslipId);
    }
} 