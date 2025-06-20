using ERP.HRService.Interfaces;
using ERP.HRService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.HRService.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repository;
        public DepartmentService(IDepartmentRepository repository) { _repository = repository; }

        public Task<Department?> GetByIdAsync(string id) => _repository.GetByIdAsync(id);
        public Task<IEnumerable<Department>> GetAllAsync() => _repository.GetAllAsync();
        public Task AddAsync(Department department) => _repository.AddAsync(department);
        public Task UpdateAsync(Department department) => _repository.UpdateAsync(department);
        public Task DeleteAsync(string id) => _repository.DeleteAsync(id);
    }
}

 