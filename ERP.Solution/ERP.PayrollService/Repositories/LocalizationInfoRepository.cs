using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ERP.PayrollService.Models;
using ERP.PayrollService.Interfaces;
using ERP.PayrollService.Data;

namespace ERP.PayrollService.Repositories
{
    public class LocalizationInfoRepository : ILocalizationInfoRepository
    {
        private readonly PayrollDbContext _context;
        public LocalizationInfoRepository(PayrollDbContext context)
        {
            _context = context;
        }
        public async Task<LocalizationInfo> GetByIdAsync(int id) => await _context.LocalizationInfos.FindAsync(id);
        public async Task<IEnumerable<LocalizationInfo>> GetAllAsync() => await _context.LocalizationInfos.ToListAsync();
        public async Task<LocalizationInfo> AddAsync(LocalizationInfo info)
        {
            _context.LocalizationInfos.Add(info);
            await _context.SaveChangesAsync();
            return info;
        }
        public async Task<LocalizationInfo> UpdateAsync(LocalizationInfo info)
        {
            _context.LocalizationInfos.Update(info);
            await _context.SaveChangesAsync();
            return info;
        }
        public async Task DeleteAsync(int id)
        {
            var info = await _context.LocalizationInfos.FindAsync(id);
            if (info != null)
            {
                _context.LocalizationInfos.Remove(info);
                await _context.SaveChangesAsync();
            }
        }
    }
} 