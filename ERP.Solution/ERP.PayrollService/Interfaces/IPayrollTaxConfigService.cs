using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.ViewModels;

namespace ERP.PayrollService.Interfaces
{
    public interface IPayrollTaxConfigService
    {
        Task<IEnumerable<PayrollTaxConfigViewModel>> GetAllAsync();
        Task<PayrollTaxConfigViewModel> GetByIdAsync(int id);
        Task<PayrollTaxConfigViewModel> CreateAsync(PayrollTaxConfigViewModel ViewModel);
        Task<PayrollTaxConfigViewModel> UpdateAsync(  PayrollTaxConfigViewModel ViewModel);
        Task<bool> DeleteAsync(int id);
    }
} 