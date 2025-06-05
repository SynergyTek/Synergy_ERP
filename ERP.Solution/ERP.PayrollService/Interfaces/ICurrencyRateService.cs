using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.Models;

namespace ERP.PayrollService.Interfaces
{
    public interface ICurrencyRateService
    {
        Task<CurrencyRate> GetByIdAsync(int id);
        Task<IEnumerable<CurrencyRate>> GetAllAsync();
        Task<CurrencyRate> CreateAsync(CurrencyRate rate);
        Task<CurrencyRate> UpdateAsync(CurrencyRate rate);
        Task DeleteAsync(int id);
    }
} 