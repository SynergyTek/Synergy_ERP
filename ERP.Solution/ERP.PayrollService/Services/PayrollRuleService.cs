using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.ViewModels;
using ERP.PayrollService.Interfaces;
using ERP.PayrollService.Models;

namespace ERP.PayrollService.Services
{
    public class PayrollRuleService : IPayrollRuleService
    {
        private readonly IPayrollRuleRepository _repo;
        public PayrollRuleService(IPayrollRuleRepository repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<PayrollRuleViewModel>> GetAllAsync()
        {
            var rules = await _repo.GetAllAsync();
            var vms = new List<PayrollRuleViewModel>();
            foreach (var r in rules)
            {
                vms.Add(new PayrollRuleViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Formula = r.Formula,
                    Sequence = r.Sequence
                });
            }
            return vms;
        }
        public async Task<PayrollRuleViewModel> GetByIdAsync(int id)
        {
            var r = await _repo.GetByIdAsync(id);
            if (r == null) return null;
            return new PayrollRuleViewModel
            {
                Id = r.Id,
                Name = r.Name,
                Formula = r.Formula,
                Sequence = r.Sequence
            };
        }
        public async Task<PayrollRuleViewModel> CreateAsync(PayrollRuleViewModel vm)
        {
            var entity = new PayrollRule
            {
                Name = vm.Name,
                Formula = vm.Formula,
                Sequence = vm.Sequence
            };
            var created = await _repo.AddAsync(entity);
            vm.Id = created.Id;
            return vm;
        }
        public async Task<PayrollRuleViewModel> UpdateAsync(PayrollRuleViewModel vm)
        {
            var entity = await _repo.GetByIdAsync(vm.Id);
            if (entity == null) return null;
            entity.Name = vm.Name;
            entity.Formula = vm.Formula;
            entity.Sequence = vm.Sequence;
            await _repo.UpdateAsync(entity);
            return vm;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }
    }
} 