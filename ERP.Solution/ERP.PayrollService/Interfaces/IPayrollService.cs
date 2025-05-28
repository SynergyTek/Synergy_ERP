using System.Threading.Tasks;
using ERP.PayrollService.ViewModels;
using System.Collections.Generic;

namespace ERP.PayrollService.Interfaces
{
    public interface IPayrollService
    {
        Task<IEnumerable<PayrollStructureViewModel>> GetPayrollStructuresAsync();
        Task<PayrollStructureViewModel> GetPayrollStructureByIdAsync(int id);
        Task<PayrollStructureViewModel> CreatePayrollStructureAsync(PayrollStructureViewModel vm);
        Task<PayrollStructureViewModel> UpdatePayrollStructureAsync(PayrollStructureViewModel vm);
        Task<bool> DeletePayrollStructureAsync(int id);
    }
} 