using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.Models;
using ERP.PayrollService.Interfaces;

namespace ERP.PayrollService.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _repo;
        public LoanService(ILoanRepository repo)
        {
            _repo = repo;
        }
        public Task<Loan> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<IEnumerable<Loan>> GetAllAsync() => _repo.GetAllAsync();
        public Task<Loan> CreateAsync(Loan loan) => _repo.AddAsync(loan);
        public Task<Loan> UpdateAsync(Loan loan) => _repo.UpdateAsync(loan);
        public Task DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
} 