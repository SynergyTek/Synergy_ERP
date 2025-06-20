using ERP.HRService.Data;
using ERP.HRService.Interfaces;
using ERP.HRService.Models;
using Microsoft.EntityFrameworkCore;

namespace ERP.HRService.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly HRDbContext _context;
        public AttendanceRepository(HRDbContext context) { _context = context; }

        public async Task<Attendance?> GetByIdAsync(string id) =>
            await _context.Attendances
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(a => a.Id == id);

        public async Task<IEnumerable<Attendance>> GetAllAsync() =>
            await _context.Attendances
                .Include(a => a.Employee)
                .ToListAsync();

        public async Task AddAsync(Attendance attendance)
        {
            await _context.Attendances.AddAsync(attendance);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Attendance attendance)
        {
            _context.Attendances.Update(attendance);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await _context.Attendances.FindAsync(id);
            if (entity != null)
            {
                _context.Attendances.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
} 