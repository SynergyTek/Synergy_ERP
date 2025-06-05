using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.Models;
using ERP.PayrollService.Interfaces;

namespace ERP.PayrollService.Services
{
    public class PayslipAdjustmentService : IPayslipAdjustmentService
    {
        private readonly IPayslipAdjustmentRepository _repo;
        public PayslipAdjustmentService(IPayslipAdjustmentRepository repo)
        {
            _repo = repo;
        }
        public Task<PayslipAdjustment> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<IEnumerable<PayslipAdjustment>> GetAllAsync() => _repo.GetAllAsync();
        public Task<PayslipAdjustment> CreateAsync(PayslipAdjustment adjustment) => _repo.AddAsync(adjustment);
        public Task<PayslipAdjustment> UpdateAsync(PayslipAdjustment adjustment) => _repo.UpdateAsync(adjustment);
        public Task DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
} 