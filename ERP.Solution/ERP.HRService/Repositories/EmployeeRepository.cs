using ERP.HRService.Data;
using ERP.HRService.Interfaces;
using ERP.HRService.Models;
using Microsoft.EntityFrameworkCore;

namespace ERP.HRService.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HRDbContext _context;
        public EmployeeRepository(HRDbContext context) { _context = context; }

        public async Task<Employee?> GetByIdAsync(string id) =>
            await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Job)
                .Include(e => e.Contracts)
                .Include(e => e.Attendances)
                .Include(e => e.Leaves)
                .Include(e => e.EmployeeSkills).ThenInclude(es => es.Skill)
                .FirstOrDefaultAsync(e => e.Id == id);

        public async Task<IEnumerable<Employee>> GetAllAsync() =>
            await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Job)
                .Include(e => e.Contracts)
                .Include(e => e.Attendances)
                .Include(e => e.Leaves)
                .Include(e => e.EmployeeSkills).ThenInclude(es => es.Skill)
                .ToListAsync();

        public async Task AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await _context.Employees.FindAsync(id);
            if (entity != null)
            {
                _context.Employees.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
} 