using ERP.HRService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.HRService.Interfaces
{
    public interface IEmployeeService
    {
        Task<Employee?> GetByIdAsync(string id);
        Task<IEnumerable<Employee>> GetAllAsync();
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(string id);
    }
} 