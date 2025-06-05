using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.Models;

namespace ERP.PayrollService.Interfaces
{
    public interface IRetroactiveChangeLogService
    {
        Task<RetroactiveChangeLog> GetByIdAsync(int id);
        Task<IEnumerable<RetroactiveChangeLog>> GetAllAsync();
        Task<RetroactiveChangeLog> CreateAsync(RetroactiveChangeLog log);
        Task<RetroactiveChangeLog> UpdateAsync(RetroactiveChangeLog log);
        Task DeleteAsync(int id);
    }
} 