using ERP.HRService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.HRService.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<Department> GetByIdAsync(string id);
        Task<IEnumerable<Department>> GetAllAsync();
        Task AddAsync(Department department);
        Task UpdateAsync(Department department);
        Task DeleteAsync(string id);
    }
} 