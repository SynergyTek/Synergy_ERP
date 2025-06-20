using ERP.HRService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.HRService.Interfaces
{
    public interface IJobService
    {
        Task<Job?> GetByIdAsync(string id);
        Task<IEnumerable<Job>> GetAllAsync();
        Task AddAsync(Job job);
        Task UpdateAsync(Job job);
        Task DeleteAsync(string id);
    }
} 