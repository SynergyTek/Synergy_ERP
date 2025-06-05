using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ERP.PayrollService.Models;
using ERP.PayrollService.Interfaces;
using System.Linq;
using ERP.PayrollService.Data;

namespace ERP.PayrollService.Repositories
{
    public class PayslipStatusChangeLogRepository : IPayslipStatusChangeLogRepository
    {
        private readonly PayrollDbContext _context;
        public PayslipStatusChangeLogRepository(PayrollDbContext context)
        {
            _context = context;
        }
        public async Task<PayslipStatusChangeLog> GetByIdAsync(int id) => await _context.PayslipStatusChangeLogs.FindAsync(id);
        public async Task<IEnumerable<PayslipStatusChangeLog>> GetAllAsync() => await _context.PayslipStatusChangeLogs.ToListAsync();
        public async Task<PayslipStatusChangeLog> AddAsync(PayslipStatusChangeLog log)
        {
            _context.PayslipStatusChangeLogs.Add(log);
            await _context.SaveChangesAsync();
            return log;
        }
        public async Task<IEnumerable<PayslipStatusChangeLog>> GetByPayslipIdAsync(int payslipId)
        {
            return await _context.PayslipStatusChangeLogs.Where(l => l.PayslipId == payslipId).ToListAsync();
        }
    }
} 