using ERP.HRService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.HRService.Interfaces
{
    public interface IAttendanceRepository
    {
        Task<Attendance?> GetByIdAsync(string id);
        Task<IEnumerable<Attendance>> GetAllAsync();
        Task AddAsync(Attendance attendance);
        Task UpdateAsync(Attendance attendance);
        Task DeleteAsync(string id);
    }
} 