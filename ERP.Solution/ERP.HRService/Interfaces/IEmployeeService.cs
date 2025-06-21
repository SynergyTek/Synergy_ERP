using ERP.HRService.Models;
using ERP.HRService.ViewModels;
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
        
        // New optimized methods for filtered queries
        Task<(IEnumerable<EmployeeViewModel> Data, int TotalCount)> GetFilteredEmployeesAsync(
            string? filter = null,
            int page = 1,
            int pageSize = 20,
            string? fields = null,
            string? sortField = null,
            string? sortOrder = null);
            
        Task<IEnumerable<EmployeeViewModel>> GetEmployeesWithLookupsAsync(
            string? filter = null,
            int page = 1,
            int pageSize = 20,
            string? fields = null,
            string? sortField = null,
            string? sortOrder = null);
    }
} 