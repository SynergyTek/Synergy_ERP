using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ERP.PayrollService.Models;
using ERP.PayrollService.Interfaces;
using ERP.PayrollService.Data;

namespace ERP.PayrollService.Repositories
{
    public class LeaveRepository : ILeaveRepository
    {
        private readonly PayrollDbContext _context;
        public LeaveRepository(PayrollDbContext context)
        {
            _context = context;
        }
        public async Task<Leave> GetByIdAsync(int id) => await _context.Leaves.FindAsync(id);
        public async Task<IEnumerable<Leave>> GetAllAsync() => await _context.Leaves.ToListAsync();
        public async Task<Leave> AddAsync(Leave leave)
        {
            _context.Leaves.Add(leave);
            await _context.SaveChangesAsync();
            return leave;
        }
        public async Task<Leave> UpdateAsync(Leave leave)
        {
            _context.Leaves.Update(leave);
            await _context.SaveChangesAsync();
            return leave;
        }
        public async Task DeleteAsync(int id)
        {
            var leave = await _context.Leaves.FindAsync(id);
            if (leave != null)
            {
                _context.Leaves.Remove(leave);
                await _context.SaveChangesAsync();
            }
        }
    }
} 