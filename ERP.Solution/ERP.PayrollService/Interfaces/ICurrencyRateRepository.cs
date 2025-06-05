using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.Models;

namespace ERP.PayrollService.Interfaces
{
    public interface ICurrencyRateRepository
    {
        Task<IEnumerable<CurrencyRate>> GetAllAsync();
        Task<CurrencyRate> GetByIdAsync(int id);
        Task<CurrencyRate> AddAsync(CurrencyRate rate);
        Task<CurrencyRate> UpdateAsync(CurrencyRate rate);
        Task DeleteAsync(int id);
    }
} 