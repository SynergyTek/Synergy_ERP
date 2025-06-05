using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.Models;

namespace ERP.PayrollService.Interfaces
{
    public interface IWorkedDayInputService
    {
        Task<IEnumerable<WorkedDayInput>> GetAllAsync();
        Task<WorkedDayInput> GetByIdAsync(int id);
        Task<WorkedDayInput> CreateAsync(WorkedDayInput input);
        Task<WorkedDayInput> UpdateAsync(WorkedDayInput input);
        Task DeleteAsync(int id);
    }
} 