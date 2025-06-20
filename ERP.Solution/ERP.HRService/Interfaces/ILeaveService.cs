using ERP.HRService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.HRService.Interfaces
{
    public interface ILeaveService
    {
        Task<Leave?> GetByIdAsync(string id);
        Task<IEnumerable<Leave>> GetAllAsync();
        Task AddAsync(Leave leave);
        Task UpdateAsync(Leave leave);
        Task DeleteAsync(string id);
    }
} 