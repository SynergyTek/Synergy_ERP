using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.Models;

namespace ERP.PayrollService.Interfaces
{
    public interface IPayslipAdjustmentRepository
    {
        Task<PayslipAdjustment> GetByIdAsync(int id);
        Task<IEnumerable<PayslipAdjustment>> GetAllAsync();
        Task<PayslipAdjustment> AddAsync(PayslipAdjustment adjustment);
        Task<PayslipAdjustment> UpdateAsync(PayslipAdjustment adjustment);
        Task DeleteAsync(int id);
    }
} 