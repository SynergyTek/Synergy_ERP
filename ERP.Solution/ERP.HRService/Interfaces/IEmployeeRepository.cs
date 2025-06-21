using ERP.HRService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.HRService.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee?> GetByIdAsync(string id);
        Task<IEnumerable<Employee>> GetAllAsync();
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(string id);
        
        // New optimized methods for filtered queries
        Task<(IEnumerable<Employee> Data, int TotalCount)> GetFilteredEmployeesAsync(
            string? filter = null,
            int page = 1,
            int pageSize = 20,
            string? sortField = null,
            string? sortOrder = null);
            
        Task<IEnumerable<Employee>> GetEmployeesForLookupsAsync(
            string? filter = null,
            int page = 1,
            int pageSize = 20,
            string? sortField = null,
            string? sortOrder = null);
    }
} 