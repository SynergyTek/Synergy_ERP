using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.Recruitment.DTOs;

namespace ERP.Recruitment.Interfaces
{
    public interface IJobPositionService
    {
        Task<IEnumerable<JobPositionDto>> GetAllJobPositionsAsync();
        Task<JobPositionDto> GetJobPositionByIdAsync(int id);
        Task<JobPositionDto> CreateJobPositionAsync(CreateJobPositionDto createDto);
        Task<JobPositionDto> UpdateJobPositionAsync(int id, UpdateJobPositionDto updateDto);
        Task<bool> DeleteJobPositionAsync(int id);
        Task<IEnumerable<JobPositionDto>> GetActiveJobPositionsAsync();
        Task<int> GetTotalVacanciesAsync();
    }
} 