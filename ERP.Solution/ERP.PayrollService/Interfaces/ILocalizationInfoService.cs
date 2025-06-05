using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.Models;

namespace ERP.PayrollService.Interfaces
{
    public interface ILocalizationInfoService
    {
        Task<LocalizationInfo> GetByIdAsync(int id);
        Task<IEnumerable<LocalizationInfo>> GetAllAsync();
        Task<LocalizationInfo> CreateAsync(LocalizationInfo info);
        Task<LocalizationInfo> UpdateAsync(LocalizationInfo info);
        Task DeleteAsync(int id);
    }
} 