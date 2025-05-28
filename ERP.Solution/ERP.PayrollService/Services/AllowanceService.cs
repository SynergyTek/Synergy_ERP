using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.ViewModels;
using ERP.PayrollService.Interfaces;
using ERP.PayrollService.Models;

namespace ERP.PayrollService.Services
{
    public class AllowanceService : IAllowanceService
    {
        private readonly IAllowanceRepository _repo;
        public AllowanceService(IAllowanceRepository repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<AllowanceViewModel>> GetAllAsync()
        {
            var allowances = await _repo.GetAllAsync();
            var vms = new List<AllowanceViewModel>();
            foreach (var a in allowances)
            {
                vms.Add(new AllowanceViewModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    Amount = a.Amount
                });
            }
            return vms;
        }
        public async Task<AllowanceViewModel> GetByIdAsync(int id)
        {
            var a = await _repo.GetByIdAsync(id);
            if (a == null) return null;
            return new AllowanceViewModel
            {
                Id = a.Id,
                Name = a.Name,
                Amount = a.Amount
            };
        }
        public async Task<AllowanceViewModel> CreateAsync(AllowanceViewModel vm)
        {
            var entity = new Allowance
            {
                Name = vm.Name,
                Amount = vm.Amount
            };
            var created = await _repo.AddAsync(entity);
            vm.Id = created.Id;
            return vm;
        }
        public async Task<AllowanceViewModel> UpdateAsync(AllowanceViewModel vm)
        {
            var entity = await _repo.GetByIdAsync(vm.Id);
            if (entity == null) return null;
            entity.Name = vm.Name;
            entity.Amount = vm.Amount;
            await _repo.UpdateAsync(entity);
            return vm;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }
    }
} 