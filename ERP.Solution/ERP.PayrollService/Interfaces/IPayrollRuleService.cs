using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.ViewModels;

namespace ERP.PayrollService.Interfaces
{
    public interface IPayrollRuleService
    {
        Task<IEnumerable<PayrollRuleViewModel>> GetAllAsync();
        Task<PayrollRuleViewModel> GetByIdAsync(int id);
        Task<PayrollRuleViewModel> CreateAsync(PayrollRuleViewModel vm);
        Task<PayrollRuleViewModel> UpdateAsync(PayrollRuleViewModel vm);
        Task<bool> DeleteAsync(int id);
    }
} 