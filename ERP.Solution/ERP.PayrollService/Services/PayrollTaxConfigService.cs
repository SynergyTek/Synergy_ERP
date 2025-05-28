using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.ViewModels;
using ERP.PayrollService.Interfaces;
using ERP.PayrollService.Models;

namespace ERP.PayrollService.Services
{
    public class PayrollTaxConfigService : IPayrollTaxConfigService
    {
        private readonly IPayrollTaxConfigRepository _repo;
        public PayrollTaxConfigService(IPayrollTaxConfigRepository repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<PayrollTaxConfigViewModel>> GetAllAsync()
        {
            var configs = await _repo.GetAllAsync();
            var ViewModels = new List<PayrollTaxConfigViewModel>();
            foreach (var c in configs)
            {
                ViewModels.Add(new PayrollTaxConfigViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Rate = c.Rate,
                    Threshold = c.Threshold,
                    IsActive = c.IsActive
                });
            }
            return ViewModels;
        }
        public async Task<PayrollTaxConfigViewModel> GetByIdAsync(int id)
        {
            var c = await _repo.GetByIdAsync(id);
            if (c == null) return null;
            return new PayrollTaxConfigViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Rate = c.Rate,
                Threshold = c.Threshold,
                IsActive = c.IsActive
            };
        }
        public async Task<PayrollTaxConfigViewModel> CreateAsync(PayrollTaxConfigViewModel ViewModel)
        {
            var entity = new PayrollTaxConfig
            {
                Name = ViewModel.Name,
                Rate = ViewModel.Rate,
                Threshold = ViewModel.Threshold,
                IsActive = ViewModel.IsActive
            };
            var created = await _repo.AddAsync(entity);
            ViewModel.Id = created.Id;
            return ViewModel;
        }
        public async Task<PayrollTaxConfigViewModel> UpdateAsync(PayrollTaxConfigViewModel ViewModel)
        {
            var entity = await _repo.GetByIdAsync(ViewModel.Id);
            if (entity == null) return null;
            entity.Name = ViewModel.Name;
            entity.Rate = ViewModel.Rate;
            entity.Threshold = ViewModel.Threshold;
            entity.IsActive = ViewModel.IsActive;
            await _repo.UpdateAsync(entity);
            return ViewModel;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }
    }
} 