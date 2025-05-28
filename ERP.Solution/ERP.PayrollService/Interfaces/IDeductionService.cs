using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.ViewModels;

namespace ERP.PayrollService.Interfaces
{
    public interface IDeductionService
    {
        Task<IEnumerable<DeductionViewModel>> GetAllAsync();
        Task<DeductionViewModel> GetByIdAsync(int id);
        Task<DeductionViewModel> CreateAsync(DeductionViewModel vm);
        Task<DeductionViewModel> UpdateAsync(DeductionViewModel vm);
        Task<bool> DeleteAsync(int id);
    }
} 