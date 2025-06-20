using ERP.HRService.Interfaces;
using ERP.HRService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.HRService.Services
{
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository _repository;
        public SkillService(ISkillRepository repository) { _repository = repository; }

        public Task<Skill?> GetByIdAsync(string id) => _repository.GetByIdAsync(id);
        public Task<IEnumerable<Skill>> GetAllAsync() => _repository.GetAllAsync();
        public Task AddAsync(Skill skill) => _repository.AddAsync(skill);
        public Task UpdateAsync(Skill skill) => _repository.UpdateAsync(skill);
        public Task DeleteAsync(string id) => _repository.DeleteAsync(id);
    }
}

 