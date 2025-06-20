using ERP.HRService.Data;
using ERP.HRService.Interfaces;
using ERP.HRService.Models;
using Microsoft.EntityFrameworkCore;

namespace ERP.HRService.Repositories
{
    public class LeaveRepository : ILeaveRepository
    {
        private readonly HRDbContext _context;
        public LeaveRepository(HRDbContext context) { _context = context; }

        public async Task<Leave?> GetByIdAsync(string id) =>
            await _context.Leaves
                .Include(l => l.Employee)
                .FirstOrDefaultAsync(l => l.Id == id);

        public async Task<IEnumerable<Leave>> GetAllAsync() =>
            await _context.Leaves
                .Include(l => l.Employee)
                .ToListAsync();

        public async Task AddAsync(Leave leave)
        {
            await _context.Leaves.AddAsync(leave);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Leave leave)
        {
            _context.Leaves.Update(leave);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await _context.Leaves.FindAsync(id);
            if (entity != null)
            {
                _context.Leaves.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
} 