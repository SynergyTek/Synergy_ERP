using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ERP.HRService.Models;
using ERP.HRService.Data;
using ERP.HRService.Interfaces;

namespace ERP.HRService.Repositories
{
    public class DepartmentRepository: IDepartmentRepository
    {
        private readonly HRDbContext _context;

        public DepartmentRepository(HRDbContext context)
        {
            _context = context;
        }

        public async Task<Department> GetByIdAsync(string id) =>
            await _context.Departments
                .Include(d => d.ParentDepartment)
                .Include(d => d.ChildDepartments)
                .Include(d => d.Employees)
                .FirstOrDefaultAsync(d => d.Id == id);

        public async Task<IEnumerable<Department>> GetAllAsync() =>
            await _context.Departments.ToListAsync();

        public async Task AddAsync(Department department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Department department)
        {
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await _context.Departments.FindAsync(id);
            if (entity != null)
            {
                _context.Departments.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

    
    }
} 