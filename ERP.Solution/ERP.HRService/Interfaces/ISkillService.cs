using ERP.HRService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.HRService.Interfaces
{
    public interface ISkillService
    {
        Task<Skill?> GetByIdAsync(string id);
        Task<IEnumerable<Skill>> GetAllAsync();
        Task AddAsync(Skill skill);
        Task UpdateAsync(Skill skill);
        Task DeleteAsync(string id);
    }
} 