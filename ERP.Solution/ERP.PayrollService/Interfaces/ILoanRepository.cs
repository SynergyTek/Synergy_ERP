using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.Models;

namespace ERP.PayrollService.Interfaces
{
    public interface ILoanRepository
    {
        Task<IEnumerable<Loan>> GetAllAsync();
        Task<Loan> GetByIdAsync(int id);
        Task<Loan> AddAsync(Loan loan);
        Task<Loan> UpdateAsync(Loan loan);
        Task DeleteAsync(int id);
    }
} 