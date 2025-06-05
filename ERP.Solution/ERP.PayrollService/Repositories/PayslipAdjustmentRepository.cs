using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ERP.PayrollService.Models;
using ERP.PayrollService.Interfaces;
using ERP.PayrollService.Data;

namespace ERP.PayrollService.Repositories
{
    public class PayslipAdjustmentRepository : IPayslipAdjustmentRepository
    {
        private readonly PayrollDbContext _context;
        public PayslipAdjustmentRepository(PayrollDbContext context)
        {
            _context = context;
        }
        public async Task<PayslipAdjustment> GetByIdAsync(int id) => await _context.PayslipAdjustments.FindAsync(id);
        public async Task<IEnumerable<PayslipAdjustment>> GetAllAsync() => await _context.PayslipAdjustments.ToListAsync();
        public async Task<PayslipAdjustment> AddAsync(PayslipAdjustment adjustment)
        {
            _context.PayslipAdjustments.Add(adjustment);
            await _context.SaveChangesAsync();
            return adjustment;
        }
        public async Task<PayslipAdjustment> UpdateAsync(PayslipAdjustment adjustment)
        {
            _context.PayslipAdjustments.Update(adjustment);
            await _context.SaveChangesAsync();
            return adjustment;
        }
        public async Task DeleteAsync(int id)
        {
            var adjustment = await _context.PayslipAdjustments.FindAsync(id);
            if (adjustment != null)
            {
                _context.PayslipAdjustments.Remove(adjustment);
                await _context.SaveChangesAsync();
            }
        }
    }
} 