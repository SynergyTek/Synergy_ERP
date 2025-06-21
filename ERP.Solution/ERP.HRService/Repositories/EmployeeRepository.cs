using ERP.HRService.Data;
using ERP.HRService.Helpers;
using ERP.HRService.Interfaces;
using ERP.HRService.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ERP.HRService.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HRDbContext _context;
        private static readonly HashSet<string> AllowedFields = new() { "name", "department", "salary", "joiningDate", "job" };
        
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

        public async Task<(IEnumerable<Employee> Data, int TotalCount)> GetFilteredEmployeesAsync(
            string? filter = null,
            int page = 1,
            int pageSize = 20,
            string? sortField = null,
            string? sortOrder = null)
        {
            var query = _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Job)
                .Include(e => e.Contracts)
                .Include(e => e.Attendances)
                .Include(e => e.Leaves)
                .Include(e => e.EmployeeSkills).ThenInclude(es => es.Skill)
                .AsQueryable();

            // Apply filters at database level
            if (!string.IsNullOrWhiteSpace(filter))
            {
                var filterObj = JsonSerializer.Deserialize<LogicalFilter>(filter);
                query = ApplyJsonFilter(query, filterObj);
            }

            // Apply sorting at database level
            query = ApplySorting(query, sortField, sortOrder);

            // Get total count before pagination
            var totalCount = await query.CountAsync();

            // Apply pagination
            var data = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (data, totalCount);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesForLookupsAsync(
            string? filter = null,
            int page = 1,
            int pageSize = 20,
            string? sortField = null,
            string? sortOrder = null)
        {
            var query = _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Job)
                .Include(e => e.Contracts)
                .Include(e => e.Attendances)
                .Include(e => e.Leaves)
                .Include(e => e.EmployeeSkills).ThenInclude(es => es.Skill)
                .AsQueryable();

            // Apply filters at database level
            if (!string.IsNullOrWhiteSpace(filter))
            {
                var filterObj = JsonSerializer.Deserialize<LogicalFilter>(filter);
                query = ApplyJsonFilter(query, filterObj);
            }

            // Apply sorting at database level
            query = ApplySorting(query, sortField, sortOrder);

            // Apply pagination
            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        // Helper method for applying JSON filters at database level
        private IQueryable<Employee> ApplyJsonFilter(IQueryable<Employee> query, LogicalFilter? filter)
        {
            if (filter == null) return query;
            
            // Only support AND for simplicity; extend as needed
            if (filter.And != null)
            {
                foreach (var cond in filter.And)
                {
                    foreach (var field in cond.Keys)
                    {
                        if (!AllowedFields.Contains(field)) continue;
                        var value = cond[field];
                        
                        if (value.Eq != null)
                            query = query.WhereDynamic(field, "==", value.Eq);
                        if (value.Gt != null)
                            query = query.WhereDynamic(field, ">", value.Gt);
                        if (value.Gte != null)
                            query = query.WhereDynamic(field, ">=", value.Gte);
                        if (value.Lt != null)
                            query = query.WhereDynamic(field, "<", value.Lt);
                        if (value.Lte != null)
                            query = query.WhereDynamic(field, "<=", value.Lte);
                        if (value.Neq != null)
                            query = query.WhereDynamic(field, "!=", value.Neq);
                        if (value.In != null && value.In.Count > 0)
                            query = query.WhereDynamicIn(field, value.In);
                    }
                }
            }
            // TODO: Add OR support if needed
            return query;
        }

        // Helper method for applying sorting at database level
        private IQueryable<Employee> ApplySorting(IQueryable<Employee> query, string? sortField, string? sortOrder)
        {
            if (string.IsNullOrWhiteSpace(sortField) || string.IsNullOrWhiteSpace(sortOrder)) return query;
            
            // Validate allowed fields
            if (!AllowedFields.Contains(sortField)) return query;
            
            return sortOrder.ToLower() == "desc"
                ? query.OrderByDescendingDynamic(sortField)
                : query.OrderByDynamic(sortField);
        }
    }
} 