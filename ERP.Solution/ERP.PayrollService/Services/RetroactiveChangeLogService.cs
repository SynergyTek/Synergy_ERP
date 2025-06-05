using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.Models;
using ERP.PayrollService.Interfaces;

namespace ERP.PayrollService.Services
{
    public class RetroactiveChangeLogService : IRetroactiveChangeLogService
    {
        private readonly IRetroactiveChangeLogRepository _repo;
        public RetroactiveChangeLogService(IRetroactiveChangeLogRepository repo)
        {
            _repo = repo;
        }
        public Task<RetroactiveChangeLog> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<IEnumerable<RetroactiveChangeLog>> GetAllAsync() => _repo.GetAllAsync();
        public Task<RetroactiveChangeLog> CreateAsync(RetroactiveChangeLog log) => _repo.AddAsync(log);
        public Task<RetroactiveChangeLog> UpdateAsync(RetroactiveChangeLog log) => _repo.UpdateAsync(log);
        public Task DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
} 