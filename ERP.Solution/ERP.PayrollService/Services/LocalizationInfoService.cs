using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.Models;
using ERP.PayrollService.Interfaces;

namespace ERP.PayrollService.Services
{
    public class LocalizationInfoService : ILocalizationInfoService
    {
        private readonly ILocalizationInfoRepository _repo;
        public LocalizationInfoService(ILocalizationInfoRepository repo)
        {
            _repo = repo;
        }
        public Task<LocalizationInfo> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<IEnumerable<LocalizationInfo>> GetAllAsync() => _repo.GetAllAsync();
        public Task<LocalizationInfo> CreateAsync(LocalizationInfo info) => _repo.AddAsync(info);
        public Task<LocalizationInfo> UpdateAsync(LocalizationInfo info) => _repo.UpdateAsync(info);
        public Task DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
} 