using ERP.HRService.Interfaces;
using ERP.HRService.Models;

namespace ERP.HRService.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _repository;
        public AttendanceService(IAttendanceRepository repository) { _repository = repository; }

        public Task<Attendance?> GetByIdAsync(string id) => _repository.GetByIdAsync(id);
        public Task<IEnumerable<Attendance>> GetAllAsync() => _repository.GetAllAsync();
        public Task AddAsync(Attendance attendance) => _repository.AddAsync(attendance);
        public Task UpdateAsync(Attendance attendance) => _repository.UpdateAsync(attendance);
        public Task DeleteAsync(string id) => _repository.DeleteAsync(id);
    }
} 