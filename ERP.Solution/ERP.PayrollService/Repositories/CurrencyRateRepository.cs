using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ERP.PayrollService.Models;
using ERP.PayrollService.Interfaces;
using ERP.PayrollService.Data;

namespace ERP.PayrollService.Repositories
{
    public class CurrencyRateRepository : ICurrencyRateRepository
    {
        private readonly PayrollDbContext _context;
        public CurrencyRateRepository(PayrollDbContext context)
        {
            _context = context;
        }
        public async Task<CurrencyRate> GetByIdAsync(int id) => await _context.CurrencyRates.FindAsync(id);
        public async Task<IEnumerable<CurrencyRate>> GetAllAsync() => await _context.CurrencyRates.ToListAsync();
        public async Task<CurrencyRate> AddAsync(CurrencyRate rate)
        {
            _context.CurrencyRates.Add(rate);
            await _context.SaveChangesAsync();
            return rate;
        }
        public async Task<CurrencyRate> UpdateAsync(CurrencyRate rate)
        {
            _context.CurrencyRates.Update(rate);
            await _context.SaveChangesAsync();
            return rate;
        }
        public async Task DeleteAsync(int id)
        {
            var rate = await _context.CurrencyRates.FindAsync(id);
            if (rate != null)
            {
                _context.CurrencyRates.Remove(rate);
                await _context.SaveChangesAsync();
            }
        }
    }
} 