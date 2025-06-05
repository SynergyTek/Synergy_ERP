using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ERP.PayrollService.Models;
using ERP.PayrollService.Interfaces;
using ERP.PayrollService.Data;

namespace ERP.PayrollService.Repositories
{
    public class RetroactiveChangeLogRepository : IRetroactiveChangeLogRepository
    {
        private readonly PayrollDbContext _context;
        public RetroactiveChangeLogRepository(PayrollDbContext context)
        {
            _context = context;
        }
        public async Task<RetroactiveChangeLog> GetByIdAsync(int id) => await _context.RetroactiveChangeLogs.FindAsync(id);
        public async Task<IEnumerable<RetroactiveChangeLog>> GetAllAsync() => await _context.RetroactiveChangeLogs.ToListAsync();
        public async Task<RetroactiveChangeLog> AddAsync(RetroactiveChangeLog log)
        {
            _context.RetroactiveChangeLogs.Add(log);
            await _context.SaveChangesAsync();
            return log;
        }
        public async Task<RetroactiveChangeLog> UpdateAsync(RetroactiveChangeLog log)
        {
            _context.RetroactiveChangeLogs.Update(log);
            await _context.SaveChangesAsync();
            return log;
        }
        public async Task DeleteAsync(int id)
        {
            var log = await _context.RetroactiveChangeLogs.FindAsync(id);
            if (log != null)
            {
                _context.RetroactiveChangeLogs.Remove(log);
                await _context.SaveChangesAsync();
            }
        }
    }
} 