using ERP.HRService.Data;
using ERP.HRService.Interfaces;
using ERP.HRService.Models;
using Microsoft.EntityFrameworkCore;

namespace ERP.HRService.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly HRDbContext _context;
        public SkillRepository(HRDbContext context) { _context = context; }

        public async Task<Skill?> GetByIdAsync(string id) =>
            await _context.Skills
                .Include(s => s.EmployeeSkills)
                .ThenInclude(es => es.Employee)
                .FirstOrDefaultAsync(s => s.Id == id);

        public async Task<IEnumerable<Skill>> GetAllAsync() =>
            await _context.Skills
                .Include(s => s.EmployeeSkills)
                .ThenInclude(es => es.Employee)
                .ToListAsync();

        public async Task AddAsync(Skill skill)
        {
            await _context.Skills.AddAsync(skill);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Skill skill)
        {
            _context.Skills.Update(skill);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await _context.Skills.FindAsync(id);
            if (entity != null)
            {
                _context.Skills.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
} 