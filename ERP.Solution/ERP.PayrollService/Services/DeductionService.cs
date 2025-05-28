using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.ViewModels;
using ERP.PayrollService.Interfaces;
using ERP.PayrollService.Models;

namespace ERP.PayrollService.Services
{
    public class DeductionService : IDeductionService
    {
        private readonly IDeductionRepository _repo;
        public DeductionService(IDeductionRepository repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<DeductionViewModel>> GetAllAsync()
        {
            var deductions = await _repo.GetAllAsync();
            var ViewModels = new List<DeductionViewModel>();
            foreach (var d in deductions)
            {
                ViewModels.Add(new DeductionViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    Amount = d.Amount
                });
            }
            return ViewModels;
        }
        public async Task<DeductionViewModel> GetByIdAsync(int id)
        {
            var d = await _repo.GetByIdAsync(id);
            if (d == null) return null;
            return new DeductionViewModel
            {
                Id = d.Id,
                Name = d.Name,
                Amount = d.Amount
            };
        }
        public async Task<DeductionViewModel> CreateAsync(DeductionViewModel ViewModel)
        {
            var entity = new Deduction
            {
                Name = ViewModel.Name,
                Amount = ViewModel.Amount
            };
            var created = await _repo.AddAsync(entity);
            ViewModel.Id = created.Id;
            return ViewModel;
        }
        public async Task<DeductionViewModel> UpdateAsync(DeductionViewModel ViewModel)
        {
            var entity = await _repo.GetByIdAsync(ViewModel.Id);
            if (entity == null) return null;
            entity.Name = ViewModel.Name;
            entity.Amount = ViewModel.Amount;
            await _repo.UpdateAsync(entity);
            return ViewModel;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }
    }
} 