using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.Models;

namespace ERP.PayrollService.Interfaces
{
    public interface IPayslipStatusChangeLogRepository
    {
        Task<PayslipStatusChangeLog> GetByIdAsync(int id);
        Task<IEnumerable<PayslipStatusChangeLog>> GetAllAsync();
        Task<PayslipStatusChangeLog> AddAsync(PayslipStatusChangeLog log);
        Task<IEnumerable<PayslipStatusChangeLog>> GetByPayslipIdAsync(int payslipId);
    }
} 