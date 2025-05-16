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
    public class JobPositionService : IJobPositionService
    {
        private readonly RecruitmentDbContext _context;

        public JobPositionService(RecruitmentDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<JobPositionDto>> GetAllJobPositionsAsync()
        {
            return await _context.JobPositions
                .Select(j => MapToDto(j))
                .ToListAsync();
        }

        public async Task<JobPositionDto> GetJobPositionByIdAsync(int id)
        {
            var jobPosition = await _context.JobPositions.FindAsync(id);
            return jobPosition != null ? MapToDto(jobPosition) : new JobPositionDto();
        }

        public async Task<JobPositionDto> CreateJobPositionAsync(CreateJobPositionDto createDto)
        {
            var jobPosition = new JobPosition
            {
                Name = createDto.Name,
                Department = createDto.Department,
                JobDescription = createDto.JobDescription,
                NumberOfVacancies = createDto.NumberOfVacancies,
                ExpectedClosingDate = createDto.ExpectedClosingDate,
                Requirements = createDto.Requirements,
                Responsibilities = createDto.Responsibilities,
                MinSalary = createDto.MinSalary,
                MaxSalary = createDto.MaxSalary,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };

            _context.JobPositions.Add(jobPosition);
            await _context.SaveChangesAsync();

            return MapToDto(jobPosition);
        }

        public async Task<JobPositionDto> UpdateJobPositionAsync(int id, UpdateJobPositionDto updateDto)
        {
            var jobPosition = await _context.JobPositions.FindAsync(id);
            if (jobPosition == null) return new JobPositionDto();

            jobPosition.Name = updateDto.Name;
            jobPosition.Department = updateDto.Department;
            jobPosition.JobDescription = updateDto.JobDescription;
            jobPosition.NumberOfVacancies = updateDto.NumberOfVacancies;
            jobPosition.ExpectedClosingDate = updateDto.ExpectedClosingDate;
            jobPosition.Requirements = updateDto.Requirements;
            jobPosition.Responsibilities = updateDto.Responsibilities;
            jobPosition.MinSalary = updateDto.MinSalary;
            jobPosition.MaxSalary = updateDto.MaxSalary;
            jobPosition.IsActive = updateDto.IsActive;
            jobPosition.UpdatedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return MapToDto(jobPosition);
        }

        public async Task<bool> DeleteJobPositionAsync(int id)
        {
            var jobPosition = await _context.JobPositions.FindAsync(id);
            if (jobPosition == null) return false;

            _context.JobPositions.Remove(jobPosition);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<JobPositionDto>> GetActiveJobPositionsAsync()
        {
            return await _context.JobPositions
                .Where(j => j.IsActive)
                .Select(j => MapToDto(j))
                .ToListAsync();
        }

        public async Task<int> GetTotalVacanciesAsync()
        {
            return await _context.JobPositions
                .Where(j => j.IsActive)
                .SumAsync(j => j.NumberOfVacancies);
        }

        private static JobPositionDto MapToDto(JobPosition jobPosition)
        {
            return new JobPositionDto
            {
                Id = jobPosition.Id,
                Name = jobPosition.Name,
                Department = jobPosition.Department,
                JobDescription = jobPosition.JobDescription,
                NumberOfVacancies = jobPosition.NumberOfVacancies,
                ExpectedClosingDate = jobPosition.ExpectedClosingDate,
                IsActive = jobPosition.IsActive,
                Requirements = jobPosition.Requirements,
                Responsibilities = jobPosition.Responsibilities,
                MinSalary = jobPosition.MinSalary,
                MaxSalary = jobPosition.MaxSalary
            };
        }
    }
} 