using ERP.API.Helpers;
using ERP.API.Models;
using ERP.HRService.Data;
using ERP.HRService.Interfaces;
using ERP.HRService.Models;
using ERP.HRService.ViewModels;
using ERP.PayrollService.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json;

namespace ERP.API.HR
{
    [ApiController]
    [Route("api/v1/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        private readonly IJobService _jobService;
        private static readonly HashSet<string> AllowedFields = new() { "name", "department", "salary", "joiningDate", "job" };
        private const int MaxPageSize = 100;
        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService, IJobService jobService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
            _jobService = jobService;
        }

        /// <summary>
        /// Returns a list of all employees in the system.
        /// LLM: Use this to retrieve all employee records.
        /// Example Request:
        ///   GET /api/v1/employee
        /// Example Response:
        ///   [
        ///     {
        ///       "id": "emp_123",
        ///       "name": "Alice Johnson",
        ///       "workEmail": "alice.johnson@example.com",
        ///       "departmentId": "dept_1",
        ///       "joiningDate": "2021-05-01",
        ///       "status": "active"
        ///     },
        ///     ...
        ///   ]
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeViewModel>>> GetAll1Employee()
        {
            var employees = await _employeeService.GetAllAsync();
            var result = employees.Select(ToViewModel);
            return Ok(result);
        }

        /// <summary>
        /// Returns a single employee by their unique ID.
        /// LLM: Use this to fetch details for a specific employee.
        /// Example Request:
        ///   GET /api/v1/employee/emp_123
        /// Example Response:
        ///   {
        ///     "id": "emp_123",
        ///     "name": "Alice Johnson",
        ///     "workEmail": "alice.johnson@example.com",
        ///     "departmentId": "dept_1",
        ///     "joiningDate": "2021-05-01",
        ///     "status": "active"
        ///   }
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeViewModel>> GetEmployeeById(string id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null) return NotFound();
            return Ok(ToViewModel(employee));
        }

        /// <summary>
        /// Creates a new employee record.
        /// LLM: Use this to add a new employee.
        /// Example Request:
        ///   POST /api/v1/employee
        ///   Body:
        ///   {
        ///     "name": "John Doe",
        ///     "workEmail": "john.doe@example.com",
        ///     "departmentId": "dept_1",
        ///     "joiningDate": "2023-01-01",
        ///     "status": "active"
        ///   }
        /// Example Response:
        ///   {
        ///     "id": "emp_456",
        ///     "name": "John Doe",
        ///     "workEmail": "john.doe@example.com",
        ///     "departmentId": "dept_1",
        ///     "joiningDate": "2023-01-01",
        ///     "status": "active"
        ///   }
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> CreateEmployee(Employee employee)
        {
            await _employeeService.AddAsync(employee);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, ToViewModel(employee));
        }

        /// <summary>
        /// Updates an existing employee by ID.
        /// LLM: Use this to modify employee details.
        /// Example Request:
        ///   PUT /api/v1/employee/emp_123
        ///   Body:
        ///   {
        ///     "name": "Jane Doe",
        ///     "status": "on-leave"
        ///   }
        /// Example Response:
        ///   204 No Content
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEmployee(string id, Employee employee)
        {
            if (id != employee.Id) return BadRequest();
            await _employeeService.UpdateAsync(employee);
            return NoContent();
        }

        /// <summary>
        /// Deletes an employee by ID.
        /// LLM: Use this to remove an employee from the system.
        /// Example Request:
        ///   DELETE /api/v1/employee/emp_123
        /// Example Response:
        ///   204 No Content
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(string id)
        {
            await _employeeService.DeleteAsync(id);
            return NoContent();
        }

        private static EmployeeViewModel ToViewModel(Employee e)
        {
            return new EmployeeViewModel
            {
                Id = e.Id,
                Name = e.Name,
                WorkEmail = e.WorkEmail,
                WorkPhone = e.WorkPhone,
                Birthday = e.Birthday,
                Gender = e.Gender,
                Department = e.Department == null ? null : new DepartmentViewModel { Id = e.Department.Id, Name = e.Department.Name },
                Job = e.Job == null ? null : new JobViewModel { Id = e.Job.Id, Name = e.Job.Name, Description = e.Job.Description },
                Contracts = e.Contracts.Select(c => new ContractViewModel { Id = c.Id, StartDate = c.StartDate, EndDate = c.EndDate, ContractType = c.ContractType, Wage = c.Wage, State = c.State }).ToList(),
                Attendances = e.Attendances.Select(a => new AttendanceViewModel { Id = a.Id, CheckIn = a.CheckIn, CheckOut = a.CheckOut, State = a.State }).ToList(),
                Leaves = e.Leaves.Select(l => new LeaveViewModel { Id = l.Id, StartDate = l.StartDate, EndDate = l.EndDate, LeaveType = l.LeaveType, State = l.State, Reason = l.Reason }).ToList(),
                Skills = e.EmployeeSkills.Select(es => new EmployeeSkillViewModel { SkillId = es.SkillId, SkillName = es.Skill?.Name ?? string.Empty, Level = es.Level }).ToList()
            };
        }

        /// <summary>
        /// Query employees with dynamic JSON filters, sorting, and pagination via GET.
        /// LLM: Use this to perform advanced queries with AND/OR filters, sorting, and pagination.
        /// Example Request:
        ///   GET /api/v1/employee/query?filter={"and":[{"department":{"eq":"Finance"}},{"joiningDate":{"gt":"2023-01-01"}},{"salary":{"gt":20000}}]}&sort={"field":"name","order":"asc"}&pagination={"page":1,"pageSize":2}
        /// Example Response:
        ///   {
        ///     "data": [
        ///       {
        ///         "id": "emp_789",
        ///         "name": "Carol Lee",
        ///         "department": "Finance",
        ///         "joiningDate": "2023-05-10",
        ///         "salary": 25000
        ///       }
        ///     ]
        ///   }
        /// </summary>
        [HttpGet("query")]
        public async Task<ActionResult<IEnumerable<EmployeeViewModel>>> QueryEmployeesGet(
            [FromQuery] string? filter = null,
            [FromQuery] string? sort = null,
            [FromQuery] string? pagination = null,
            [FromQuery] string? fields = null)
        {
            LogicalFilter? filterObj = null;
            SortOption? sortObj = null;
            Pagination? paginationObj = null;
            if (!string.IsNullOrWhiteSpace(filter))
                filterObj = JsonSerializer.Deserialize<LogicalFilter>(filter);
            if (!string.IsNullOrWhiteSpace(sort))
                sortObj = JsonSerializer.Deserialize<SortOption>(sort);
            if (!string.IsNullOrWhiteSpace(pagination))
                paginationObj = JsonSerializer.Deserialize<Pagination>(pagination);

            var query = await _employeeService.GetAllAsync();
            query = ApplyJsonFilter(query, filterObj);
            query = ApplySorting(query, sortObj);
            query = ApplyPagination(query, paginationObj);

            List<string>? selectedFields = null;
            if (!string.IsNullOrWhiteSpace(fields))
                selectedFields = fields.Split(',').Select(f => f.Trim()).ToList();

            var result = query.ToList();
            if (selectedFields != null && selectedFields.Count > 0)
            {
                var shaped = result.Select(emp =>
                {
                    var dict = new Dictionary<string, object?>();
                    var type = typeof(EmployeeViewModel);
                    var vm = ToViewModel(emp);
                    foreach (var field in selectedFields)
                    {
                        var prop = type.GetProperty(field, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                        if (prop != null)
                            dict[field] = prop.GetValue(vm);
                    }
                    return dict;
                }).ToList();
                return Ok(new { data = shaped });
            }
            return Ok(new { data = result.Select(ToViewModel).ToList() });
        }

        /// <summary>
        /// List employees with optional filters, pagination, and field selection.
        /// LLM: Use this to retrieve a filtered, paginated list of employees by department name and other criteria, and also get all departments and jobs.
        /// Example Request:
        ///   GET /api/v1/employee/list?department=Finance&fields=id,name,joiningDate&page=1&pageSize=2
        /// Example Response:
        ///   {
        ///     "data": [
        ///       {
        ///         "id": "emp_123",
        ///         "name": "Alice Johnson",
        ///         "joiningDate": "2021-05-01"
        ///       },
        ///       {
        ///         "id": "emp_456",
        ///         "name": "Bob Smith",
        ///         "joiningDate": "2022-03-15"
        ///       }
        ///     ],
        ///     "departments": [
        ///       { "id": "dept_1", "name": "Finance" },
        ///       { "id": "dept_2", "name": "HR" }
        ///     ],
        ///     "jobs": [
        ///       { "id": "job_1", "name": "Accountant" },
        ///       { "id": "job_2", "name": "Engineer" }
        ///     ]
        ///   }
        /// </summary>
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<EmployeeViewModel>>> GetEmployeeList(
            [FromQuery] string? filter = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] string? fields = null)
        {
            LogicalFilter? filterObj = null;
            if (!string.IsNullOrWhiteSpace(filter))
                filterObj = System.Text.Json.JsonSerializer.Deserialize<LogicalFilter>(filter);
            var query = await _employeeService.GetAllAsync();
            query = ApplyJsonFilter(query, filterObj);
            query = query.Skip((page - 1) * pageSize).Take(pageSize);
            var employees = query.ToList();
            List<string>? selectedFields = null;
            if (!string.IsNullOrWhiteSpace(fields))
                selectedFields = fields.Split(',').Select(f => f.Trim()).ToList();
            object data;
            if (selectedFields != null && selectedFields.Count > 0)
            {
                data = employees.Select(emp =>
                {
                    var dict = new Dictionary<string, object?>();
                    var type = typeof(EmployeeViewModel);
                    var vm = ToViewModel(emp);
                    foreach (var field in selectedFields)
                    {
                        var prop = type.GetProperty(field, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                        if (prop != null)
                            dict[field] = prop.GetValue(vm);
                    }
                    return dict;
                }).ToList();
            }
            else
            {
                data = employees.Select(ToViewModel).ToList();
            }
            var departments = (await _departmentService.GetAllAsync()).Select(d => new DepartmentViewModel { Id = d.Id, Name = d.Name }).ToList();
            var jobs = (await _jobService.GetAllAsync()).Select(j => new JobViewModel { Id = j.Id, Name = j.Name, Description = j.Description }).ToList();
            return Ok(new { data, departments, jobs });
        }

        /// <summary>
        /// Aggregate employees based on the given request.
        /// 
        /// Example:
        /// POST /api/employees/aggregate
        /// Body:
        /// {
        ///   "filter": { "and": [ { "department": { "eq": "Information Technology" } } ] },
        ///   "groupBy": ["department"],
        ///   "aggregates": [ { "operation": "count", "field": "id" } ]
        /// }
        /// </summary>
        [HttpGet("aggregate")]
        public async Task<ActionResult<IEnumerable<EmployeeViewModel>>> GetAggregateEmployees(
            [FromQuery] string group_by,
            [FromQuery] string aggregate_field,
            [FromQuery] string aggregate_op,
            [FromQuery] string? filter = null,
            [FromQuery] string? fields = null)
        {
            LogicalFilter? filterObj = null;
            if (!string.IsNullOrWhiteSpace(filter))
                filterObj = System.Text.Json.JsonSerializer.Deserialize<LogicalFilter>(filter);
            var query = await _employeeService.GetAllAsync();
            query = ApplyJsonFilter(query, filterObj);
            List<string>? selectedFields = null;
            if (!string.IsNullOrWhiteSpace(fields))
                selectedFields = fields.Split(',').Select(f => f.Trim()).ToList();
            if (group_by != "department" || aggregate_field != "id" || aggregate_op != "count")
                return BadRequest("Only group_by=department and aggregate_field=id&aggregate_op=count are supported in this demo.");
            var grouped = query.GroupBy(e => e.Department)
                .Select(g => new {
                    department = g.Key == null ? null : new DepartmentViewModel { Id = g.Key.Id, Name = g.Key.Name },
                    count_id = g.Count(),
                    employees = g.Select(ToViewModel).ToList()
                })
                .ToList();
            var result = grouped.Select(g => new Dictionary<string, object?>
            {
                ["department"] = g.department,
                ["count_id"] = g.count_id,
                ["employees"] = selectedFields != null && selectedFields.Count > 0
                    ? g.employees.Select(emp => {
                        var dict = new Dictionary<string, object?>();
                        var type = typeof(EmployeeViewModel);
                        foreach (var field in selectedFields)
                        {
                            var prop = type.GetProperty(field, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                            if (prop != null)
                                dict[field] = prop.GetValue(emp);
                        }
                        return dict;
                    }).ToList()
                    : g.employees
            }).ToList();
            return Ok(new { data = result });
        }

        /// <summary>
        /// Returns the count of employees matching optional filters.
        /// LLM: Use this to get the number of employees that match specific criteria.
        /// Example Request:
        ///   GET /api/v1/employee/count?department_id=dept_1&status=active
        /// Example Response:
        ///   {
        ///     "count": 42
        ///   }
        /// </summary>
        [HttpGet("count")]
        public async Task<ActionResult> CountEmployeesGet(
            [FromQuery] string? filter = null,
            [FromQuery] string? fields = null)
        {
            LogicalFilter? filterObj = null;
            if (!string.IsNullOrWhiteSpace(filter))
                filterObj = System.Text.Json.JsonSerializer.Deserialize<LogicalFilter>(filter);
            var query = await _employeeService.GetAllAsync();
            query = ApplyJsonFilter(query, filterObj);
            // The count endpoint typically doesn't use fields, but you can add logic if needed
            var count = query.Count();
            return Ok(new { count });
        }

        // Helper methods for filtering, sorting, and pagination
        private IEnumerable<T> ApplyJsonFilter<T>(IEnumerable<T> query, LogicalFilter? filter)
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

        private IEnumerable<T> ApplySorting<T>(IEnumerable<T> query, SortOption? sort)
        {
            if (sort == null || !AllowedFields.Contains(sort.Field)) return query;
            return sort.Order.ToLower() == "desc"
                ? query.OrderByDescendingDynamic(sort.Field)
                : query.OrderByDynamic(sort.Field);
        }

        private IEnumerable<T> ApplyPagination<T>(IEnumerable<T> query, Pagination? pagination)
        {
            if (pagination == null) return query.Take(20);
            var page = Math.Max(1, pagination.Page);
            var pageSize = Math.Min(MaxPageSize, Math.Max(1, pagination.PageSize));
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        /// <summary>
        /// Returns the schema for the employee entity, including fields, relationships, endpoints, and example usage.
        /// LLM: Use this to understand available features, fields, and relationships for the employee resource.
        /// Example Request:
        ///   GET /api/v1/employee/schema
        /// Example Response:
        ///   {
        ///     "entity": "employee",
        ///     "description": "Master data for employees in the organization.",
        ///     "version": "1.0",
        ///     "endpoints": [ ... ],
        ///     "primary_key": "Id",
        ///     "fields": [ ... ]
        ///   }
        /// </summary>
        [HttpGet("schema")]
        public IActionResult GetEmployeeSchema()
        {
            var type = typeof(Employee);
            var properties = type.GetProperties();
            var fields = new List<object>();

            foreach (var prop in properties)
            {
                // Skip navigation properties (collections or complex types)
                if (prop.PropertyType.Namespace != "System" && !prop.PropertyType.IsPrimitive && prop.PropertyType != typeof(string))
                {
                    // If it's a collection, treat as relationship
                    if (typeof(System.Collections.IEnumerable).IsAssignableFrom(prop.PropertyType) && prop.PropertyType != typeof(string))
                    {
                        fields.Add(new
                        {
                            name = prop.Name,
                            type = "collection",
                            related_entity = prop.PropertyType.IsGenericType
                                ? prop.PropertyType.GetGenericArguments()[0].Name
                                : "unknown"
                        });
                    }
                    else
                    {
                        // Single navigation property
                        fields.Add(new
                        {
                            name = prop.Name,
                            type = "object",
                            related_entity = prop.PropertyType.Name
                        });
                    }
                    continue;
                }

                var field = new Dictionary<string, object?>
                {
                    ["name"] = prop.Name,
                    ["type"] = Nullable.GetUnderlyingType(prop.PropertyType)?.Name?.ToLower() ?? prop.PropertyType.Name.ToLower(),
                    ["required"] = prop.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RequiredAttribute), false).Any()
                };

                // Foreign key detection (simple version)
                if (prop.Name.EndsWith("Id") && prop.Name != "Id")
                {
                    var relatedEntity = prop.Name.Substring(0, prop.Name.Length - 2);
                    field["foreign_key"] = new { entity = relatedEntity.ToLower(), field = "id" };
                }

                fields.Add(field);
            }

            // Discover endpoints from controller actions
            var controllerType = typeof(EmployeeController);
            var endpoints = controllerType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .Where(m => m.GetCustomAttributes<HttpMethodAttribute>().Any())
                .Select(m =>
                {
                    var httpAttr = m.GetCustomAttributes<HttpMethodAttribute>().First();
                    var route = httpAttr.Template != null
                        ? $"/api/v1/employee/{httpAttr.Template}".Replace("//", "/")
                        : "/api/v1/employee";
                    return new
                    {
                        name = m.Name,
                        http_method = httpAttr.HttpMethods.First(),
                        route
                    };
                }).ToList();

            var schema = new
            {
                entity = "employee",
                description = "Master data for employees in the organization.",
                version = "1.0",
                endpoints = endpoints,
                primary_key = "Id",
                fields = fields
            };

            return Ok(schema);
        }
    }
} 