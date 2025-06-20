using ERP.API.Helpers;
using ERP.API.Models;
using ERP.PayrollService.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.API.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeeQueryController : ControllerBase
    {
        private readonly PayrollDbContext _dbContext;
        private static readonly HashSet<string> AllowedFields = new() { "name", "department", "salary", "joiningDate", "job" };
        private const int MaxPageSize = 100;

        public EmployeeQueryController(PayrollDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Query employees with dynamic JSON filters, sorting, and pagination.
        /// </summary>
        /// <remarks>
        /// Filters using JSON structure like:
        /// {
        ///   "filter": {
        ///     "and": [
        ///       { "department": { "eq": "Finance" } },
        ///       { "salary": { "gt": "20000" } }
        ///     ]
        ///   },
        ///   "sort": { "field": "name", "order": "asc" },
        ///   "pagination": { "page": 1, "pageSize": 20 }
        /// }
        /// </remarks>
        [HttpPost("GetEmployee")]
        public async Task<IActionResult> QueryEmployees([FromBody] FilterRequest request)
        {
            var query = _dbContext.Employees.AsQueryable();
            query = ApplyJsonFilter(query, request.Filter);
            query = ApplySorting(query, request.Sort);
            query = ApplyPagination(query, request.Pagination);
            var result = await query.ToListAsync();
            return Ok(result);
        }

        /// <summary>
        /// Query employee salaries with dynamic JSON filters.
        /// </summary>
        [HttpPost("GetSalary")]
        public async Task<IActionResult> QuerySalaries([FromBody] FilterRequest request)
        {
            var query = _dbContext.Employees.Select(e => new { e.Id, e.Name, Salary = e.BaseSalary }).AsQueryable();
            query = ApplyJsonFilter(query, request.Filter);
            query = ApplySorting(query, request.Sort);
            query = ApplyPagination(query, request.Pagination);
            var result = await query.ToListAsync();
            return Ok(result);
        }

        /// <summary>
        /// Query employee jobs with dynamic JSON filters.
        /// </summary>
        [HttpPost("GetJob")]
        public async Task<IActionResult> QueryJobs([FromBody] FilterRequest request)
        {
            var query = _dbContext.Employees.Select(e => new { e.Id, e.Name, Job = e.Position }).AsQueryable();
            query = ApplyJsonFilter(query, request.Filter);
            query = ApplySorting(query, request.Sort);
            query = ApplyPagination(query, request.Pagination);
            var result = await query.ToListAsync();
            return Ok(result);
        }

        // Helper methods for filtering, sorting, and pagination
        private IQueryable<T> ApplyJsonFilter<T>(IQueryable<T> query, LogicalFilter? filter)
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

        private IQueryable<T> ApplySorting<T>(IQueryable<T> query, SortOption? sort)
        {
            if (sort == null || !AllowedFields.Contains(sort.Field)) return query;
            return sort.Order.ToLower() == "desc"
                ? query.OrderByDescendingDynamic(sort.Field)
                : query.OrderByDynamic(sort.Field);
        }

        private IQueryable<T> ApplyPagination<T>(IQueryable<T> query, Pagination? pagination)
        {
            if (pagination == null) return query.Take(20);
            var page = Math.Max(1, pagination.Page);
            var pageSize = Math.Min(MaxPageSize, Math.Max(1, pagination.PageSize));
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }

    
} 