using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.Models;

namespace ERP.PayrollService.Interfaces
{
    public interface ILeaveService
    {
        Task<Leave> GetByIdAsync(int id);
        Task<IEnumerable<Leave>> GetAllAsync();
        Task<Leave> CreateAsync(Leave leave);
        Task<Leave> UpdateAsync(Leave leave);
        Task DeleteAsync(int id);
    }
} 