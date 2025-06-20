using ERP.HRService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.HRService.Interfaces
{
    public interface IContractRepository
    {
        Task<Contract?> GetByIdAsync(string id);
        Task<IEnumerable<Contract>> GetAllAsync();
        Task AddAsync(Contract contract);
        Task UpdateAsync(Contract contract);
        Task DeleteAsync(string id);
    }
} 