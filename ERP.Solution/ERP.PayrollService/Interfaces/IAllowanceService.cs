using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.ViewModels;

namespace ERP.PayrollService.Interfaces
{
    public interface IAllowanceService
    {
        Task<IEnumerable<AllowanceViewModel>> GetAllAsync();
        Task<AllowanceViewModel> GetByIdAsync(int id);
        Task<AllowanceViewModel> CreateAsync(AllowanceViewModel vm);
        Task<AllowanceViewModel> UpdateAsync(AllowanceViewModel vm);
        Task<bool> DeleteAsync(int id);
    }
} 