using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.ViewModels;
using ERP.PayrollService.Interfaces;
using ERP.PayrollService.Models;

namespace ERP.PayrollService.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repo;
        public EmployeeService(IEmployeeRepository repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<EmployeeViewModel>> GetAllAsync()
        {
            var employees = await _repo.GetAllAsync();
            var vms = new List<EmployeeViewModel>();
            foreach (var e in employees)
            {
                vms.Add(new EmployeeViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Department = e.Department,
                    Position = e.Position,
                    BaseSalary = e.BaseSalary,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Address = e.Address,
                    Email = e.Email,
                });
            }
            return vms;
        }
        public async Task<EmployeeViewModel> GetByIdAsync(int id)
        {
            var e = await _repo.GetByIdAsync(id);
            if (e == null) return null;
            return new EmployeeViewModel
            {
                Name = e.Name,
                Department = e.Department,
                Position = e.Position,
                BaseSalary = e.BaseSalary,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Address = e.Address,
                Email = e.Email,
            };
        }
        public async Task<EmployeeViewModel> CreateAsync(EmployeeViewModel vm)
        {
            var entity = new Employee
            {
                Name = vm.Name,
                Department = vm.Department,
                Position = vm.Position,
                BaseSalary = vm.BaseSalary,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Address = vm.Address,
                Email = vm.Email,
            };
            var created = await _repo.AddAsync(entity);
            vm.Id = created.Id;
            return vm;
        }
        public async Task<EmployeeViewModel> UpdateAsync(EmployeeViewModel vm)
        {
            var entity = await _repo.GetByIdAsync(vm.Id);
            if (entity == null) return null;
            entity.Name = vm.Name;
            entity.Department = vm.Department;
            entity.Position = vm.Position;
            entity.BaseSalary = vm.BaseSalary;
            entity.Email = vm.Email;
            entity.FirstName = vm.FirstName;
            entity.LastName = vm.LastName;
            entity.Address = vm.Address;

            await _repo.UpdateAsync(entity);
            return vm;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }
    }
}