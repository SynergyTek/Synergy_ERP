using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.ViewModels;
using ERP.PayrollService.Interfaces;
using ERP.PayrollService.Models;

namespace ERP.PayrollService.Services
{
    public class PayrollService : IPayrollService
    {
        private readonly IPayrollStructureRepository _structureRepo;

        public PayrollService(IPayrollStructureRepository structureRepo)
        {
            _structureRepo = structureRepo;
        }

        public async Task<IEnumerable<PayrollStructureViewModel>> GetPayrollStructuresAsync()
        {
            var structures = await _structureRepo.GetAllAsync();
            var vms = new List<PayrollStructureViewModel>();
            foreach (var s in structures)
            {
                vms.Add(new PayrollStructureViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description
                });
            }
            return vms;
        }

        public async Task<PayrollStructureViewModel> GetPayrollStructureByIdAsync(int id)
        {
            var s = await _structureRepo.GetByIdAsync(id);
            if (s == null) return null;
            return new PayrollStructureViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description
            };
        }

        public async Task<PayrollStructureViewModel> CreatePayrollStructureAsync(PayrollStructureViewModel vm)
        {
            var entity = new PayrollStructure
            {
                Name = vm.Name,
                Description = vm.Description
            };
            var created = await _structureRepo.AddAsync(entity);
            vm.Id = created.Id;
            return vm;
        }

        public async Task<PayrollStructureViewModel> UpdatePayrollStructureAsync(PayrollStructureViewModel vm)
        {
            var entity = await _structureRepo.GetByIdAsync(vm.Id);
            if (entity == null) return null;
            entity.Name = vm.Name;
            entity.Description = vm.Description;
            await _structureRepo.UpdateAsync(entity);
            return vm;
        }

        public async Task<bool> DeletePayrollStructureAsync(int id)
        {
            return await _structureRepo.DeleteAsync(id);
        }
    }
} 