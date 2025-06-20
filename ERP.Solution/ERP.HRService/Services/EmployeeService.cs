using ERP.HRService.Interfaces;
using ERP.HRService.Models;

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
    }
} 