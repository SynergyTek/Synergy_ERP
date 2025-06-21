using ERP.HRService.Interfaces;
using ERP.HRService.Models;
using ERP.HRService.ViewModels;
using System.Reflection;

namespace ERP.HRService.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;
        public EmployeeService(IEmployeeRepository repository) { _repository = repository; }

        public Task<Employee?> GetByIdAsync(string id) => _repository.GetByIdAsync(id);
        public Task<IEnumerable<Employee>> GetAllAsync() => _repository.GetAllAsync();
        public Task AddAsync(Employee employee) => _repository.AddAsync(employee);
        public Task UpdateAsync(Employee employee) => _repository.UpdateAsync(employee);
        public Task DeleteAsync(string id) => _repository.DeleteAsync(id);

        public async Task<(IEnumerable<EmployeeViewModel> Data, int TotalCount)> GetFilteredEmployeesAsync(
            string? filter = null,
            int page = 1,
            int pageSize = 20,
            string? fields = null,
            string? sortField = null,
            string? sortOrder = null)
        {
            var (employees, totalCount) = await _repository.GetFilteredEmployeesAsync(filter, page, pageSize, sortField, sortOrder);
            
            var viewModels = employees.Select(ToViewModel);
            
            // Apply field selection if specified
            if (!string.IsNullOrWhiteSpace(fields))
            {
                var selectedFields = fields.Split(',').Select(f => f.Trim()).ToList();
                viewModels = ApplyFieldSelection(viewModels, selectedFields);
            }

            return (viewModels, totalCount);
        }

        public async Task<IEnumerable<EmployeeViewModel>> GetEmployeesWithLookupsAsync(
            string? filter = null,
            int page = 1,
            int pageSize = 20,
            string? fields = null,
            string? sortField = null,
            string? sortOrder = null)
        {
            var employees = await _repository.GetEmployeesForLookupsAsync(filter, page, pageSize, sortField, sortOrder);
            
            var viewModels = employees.Select(ToViewModel);
            
            // Apply field selection if specified
            if (!string.IsNullOrWhiteSpace(fields))
            {
                var selectedFields = fields.Split(',').Select(f => f.Trim()).ToList();
                viewModels = ApplyFieldSelection(viewModels, selectedFields);
            }

            return viewModels;
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

        private static IEnumerable<EmployeeViewModel> ApplyFieldSelection(IEnumerable<EmployeeViewModel> viewModels, List<string> selectedFields)
        {
            return viewModels.Select(vm =>
            {
                var dict = new Dictionary<string, object?>();
                var type = typeof(EmployeeViewModel);
                
                foreach (var field in selectedFields)
                {
                    var prop = type.GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (prop != null)
                        dict[field] = prop.GetValue(vm);
                }
                
                return dict;
            }).Cast<EmployeeViewModel>();
        }
    }
} 