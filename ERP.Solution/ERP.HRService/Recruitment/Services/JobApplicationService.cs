using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ERP.Recruitment.Data;
using ERP.Recruitment.DTOs;
using ERP.Recruitment.Interfaces;
using ERP.Recruitment.Models;

namespace ERP.Recruitment.Services
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly RecruitmentDbContext _context;

        public JobApplicationService(
            RecruitmentDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<JobApplicationDto>> GetAllApplicationsAsync()
        {
            return await _context.JobApplications
                .Include(a => a.JobPosition)
                .Select(a => MapToDto(a))
                .ToListAsync();
        }

        public async Task<JobApplicationDto> GetApplicationByIdAsync(int id)
        {
            var application = await _context.JobApplications
                .Include(a => a.JobPosition)
                .FirstOrDefaultAsync(a => a.Id == id);
            return application != null ? MapToDto(application) : new JobApplicationDto();
        }

        public async Task<IEnumerable<JobApplicationDto>> GetApplicationsByJobPositionAsync(int jobPositionId)
        {
            return await _context.JobApplications
                .Include(a => a.JobPosition)
                .Where(a => a.JobPositionId == jobPositionId)
                .Select(a => MapToDto(a))
                .ToListAsync();
        }

        public async Task<JobApplicationDto> CreateApplicationAsync(CreateJobApplicationDto createDto)
        {
            var application = new JobApplication
            {
                JobPositionId = createDto.JobPositionId,
                ApplicantName = createDto.ApplicantName,
                Email = createDto.Email,
                Phone = createDto.Phone,
                ResumeUrl = createDto.ResumeUrl,
                CoverLetter = createDto.CoverLetter,
                ExpectedSalary = createDto.ExpectedSalary,
                Source = createDto.Source,
                Status = ApplicationStatus.New,
                ApplicationDate = DateTime.UtcNow,
                CurrentStage = "Initial Review"
            };

            _context.JobApplications.Add(application);
            await _context.SaveChangesAsync();

          

            return await GetApplicationByIdAsync(application.Id);
        }

        public async Task<JobApplicationDto> UpdateApplicationStatusAsync(int id, UpdateJobApplicationDto updateDto)
        {
            var application = await _context.JobApplications.FindAsync(id);
            if (application == null) return new JobApplicationDto();

            application.Status = updateDto.Status;
            application.CurrentStage = updateDto.CurrentStage;
            application.Notes = updateDto.Notes;
            application.LastStatusUpdateDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();


            return await GetApplicationByIdAsync(id);
        }

        public async Task<bool> DeleteApplicationAsync(int id)
        {
            var application = await _context.JobApplications.FindAsync(id);
            if (application == null) return false;

            _context.JobApplications.Remove(application);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<JobApplicationDto>> GetApplicationsByStatusAsync(ApplicationStatus status)
        {
            return await _context.JobApplications
                .Include(a => a.JobPosition)
                .Where(a => a.Status == status)
                .Select(a => MapToDto(a))
                .ToListAsync();
        }

        public async Task<Dictionary<ApplicationStatus, int>> GetApplicationStatisticsAsync()
        {
            return await _context.JobApplications
                .GroupBy(a => a.Status)
                .ToDictionaryAsync(
                    g => g.Key,
                    g => g.Count()
                );
        }

        private static JobApplicationDto MapToDto(JobApplication application)
        {
            return new JobApplicationDto
            {
                Id = application.Id,
                JobPositionId = application.JobPositionId,
                JobPositionName = application.JobPosition.Name,
                ApplicantName = application.ApplicantName,
                Email = application.Email,
                Phone = application.Phone,
                ResumeUrl = application.ResumeUrl,
                CoverLetter = application.CoverLetter,
                Status = application.Status,
                ApplicationDate = application.ApplicationDate,
                CurrentStage = application.CurrentStage,
                Notes = application.Notes,
                ExpectedSalary = application.ExpectedSalary,
                Source = application.Source
            };
        }
    }
} 