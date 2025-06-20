using ERP.HRService.Data;
using ERP.HRService.Interfaces;
using ERP.HRService.Models;
using Microsoft.EntityFrameworkCore;

namespace ERP.HRService.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly HRDbContext _context;
        public JobRepository(HRDbContext context) { _context = context; }

        public async Task<Job?> GetByIdAsync(string id) =>
            await _context.Jobs
                .Include(j => j.Employees)
                .FirstOrDefaultAsync(j => j.Id == id);

        public async Task<IEnumerable<Job>> GetAllAsync() =>
            await _context.Jobs
                .Include(j => j.Employees)
                .ToListAsync();

        public async Task AddAsync(Job job)
        {
            await _context.Jobs.AddAsync(job);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Job job)
        {
            _context.Jobs.Update(job);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await _context.Jobs.FindAsync(id);
            if (entity != null)
            {
                _context.Jobs.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
} 