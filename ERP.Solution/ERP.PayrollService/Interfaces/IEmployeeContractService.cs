using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.Models;

namespace ERP.PayrollService.Interfaces
{
    public interface IEmployeeContractService
    {
        Task<IEnumerable<EmployeeContract>> GetAllAsync();
        Task<EmployeeContract> GetByIdAsync(int id);
        Task<EmployeeContract> CreateAsync(EmployeeContract contract);
        Task<EmployeeContract> UpdateAsync(EmployeeContract contract);
        Task<bool> DeleteAsync(int id);
    }
} 