using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.Models;

namespace ERP.PayrollService.Interfaces
{
    public interface IPayslipLineService
    {
        Task<IEnumerable<PayslipLine>> GetAllAsync();
        Task<PayslipLine> GetByIdAsync(int id);
        Task<PayslipLine> CreateAsync(PayslipLine line);
        Task<PayslipLine> UpdateAsync(PayslipLine line);
        Task DeleteAsync(int id);
        Task<IEnumerable<PayslipLine>> GetByPayslipIdAsync(int payslipId);
    }
} 