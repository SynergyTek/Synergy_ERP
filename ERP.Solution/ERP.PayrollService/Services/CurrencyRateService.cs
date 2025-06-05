using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.Models;
using ERP.PayrollService.Interfaces;

namespace ERP.PayrollService.Services
{
    public class CurrencyRateService : ICurrencyRateService
    {
        private readonly ICurrencyRateRepository _repo;
        public CurrencyRateService(ICurrencyRateRepository repo)
        {
            _repo = repo;
        }
        public Task<CurrencyRate> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<IEnumerable<CurrencyRate>> GetAllAsync() => _repo.GetAllAsync();
        public Task<CurrencyRate> CreateAsync(CurrencyRate rate) => _repo.AddAsync(rate);
        public Task<CurrencyRate> UpdateAsync(CurrencyRate rate) => _repo.UpdateAsync(rate);
        public Task DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
} 