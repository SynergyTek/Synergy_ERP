using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.Recruitment.DTOs;
using ERP.Recruitment.Models;

namespace ERP.Recruitment.Interfaces
{
    public interface IJobApplicationService
    {
        Task<IEnumerable<JobApplicationDto>> GetAllApplicationsAsync();
        Task<JobApplicationDto> GetApplicationByIdAsync(int id);
        Task<IEnumerable<JobApplicationDto>> GetApplicationsByJobPositionAsync(int jobPositionId);
        Task<JobApplicationDto> CreateApplicationAsync(CreateJobApplicationDto createDto);
        Task<JobApplicationDto> UpdateApplicationStatusAsync(int id, UpdateJobApplicationDto updateDto);
        Task<bool> DeleteApplicationAsync(int id);
        Task<IEnumerable<JobApplicationDto>> GetApplicationsByStatusAsync(ApplicationStatus status);
        Task<Dictionary<ApplicationStatus, int>> GetApplicationStatisticsAsync();
    }
} 