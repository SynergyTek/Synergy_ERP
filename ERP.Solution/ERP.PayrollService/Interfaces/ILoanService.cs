using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.Models;

namespace ERP.PayrollService.Interfaces
{
    public interface ILoanService
    {
        Task<Loan> GetByIdAsync(int id);
        Task<IEnumerable<Loan>> GetAllAsync();
        Task<Loan> CreateAsync(Loan loan);
        Task<Loan> UpdateAsync(Loan loan);
        Task DeleteAsync(int id);
    }
} 