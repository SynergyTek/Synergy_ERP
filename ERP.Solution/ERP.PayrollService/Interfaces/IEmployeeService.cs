using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.ViewModels;

namespace ERP.PayrollService.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeViewModel>> GetAllAsync();
        Task<EmployeeViewModel> GetByIdAsync(int id);
        Task<EmployeeViewModel> CreateAsync(EmployeeViewModel vm);
        Task<EmployeeViewModel> UpdateAsync(EmployeeViewModel vm);
        Task<bool> DeleteAsync(int id);
    }
} 