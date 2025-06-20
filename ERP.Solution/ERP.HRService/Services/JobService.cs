using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.HRService.Interfaces;
using ERP.HRService.Models;
using ERP.HRService.Repositories;

namespace ERP.HRService.Services
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _repository;
        public JobService(IJobRepository repository) { _repository = repository; }

        public Task<Job?> GetByIdAsync(string id) => _repository.GetByIdAsync(id);
        public Task<IEnumerable<Job>> GetAllAsync() => _repository.GetAllAsync();
        public Task AddAsync(Job job) => _repository.AddAsync(job);
        public Task UpdateAsync(Job job) => _repository.UpdateAsync(job);
        public Task DeleteAsync(string id) => _repository.DeleteAsync(id);
    }
} 