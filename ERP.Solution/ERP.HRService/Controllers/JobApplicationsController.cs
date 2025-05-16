using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ERP.Recruitment.DTOs;
using ERP.Recruitment.Interfaces;
using ERP.Recruitment.Models;

namespace ERP.Recruitment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobApplicationsController : ControllerBase
    {
        private readonly IJobApplicationService _jobApplicationService;

        public JobApplicationsController(IJobApplicationService jobApplicationService)
        {
            _jobApplicationService = jobApplicationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobApplicationDto>>> GetAllApplications()
        {
            var applications = await _jobApplicationService.GetAllApplicationsAsync();
            return Ok(applications);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JobApplicationDto>> GetApplication(int id)
        {
            var application = await _jobApplicationService.GetApplicationByIdAsync(id);
            if (application == null)
                return NotFound();

            return Ok(application);
        }

        [HttpGet("position/{jobPositionId}")]
        public async Task<ActionResult<IEnumerable<JobApplicationDto>>> GetApplicationsByPosition(int jobPositionId)
        {
            var applications = await _jobApplicationService.GetApplicationsByJobPositionAsync(jobPositionId);
            return Ok(applications);
        }

        [HttpPost]
        public async Task<ActionResult<JobApplicationDto>> CreateApplication(CreateJobApplicationDto createDto)
        {
            var application = await _jobApplicationService.CreateApplicationAsync(createDto);
            return CreatedAtAction(nameof(GetApplication), new { id = application.Id }, application);
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult<JobApplicationDto>> UpdateApplicationStatus(int id, UpdateJobApplicationDto updateDto)
        {
            var application = await _jobApplicationService.UpdateApplicationStatusAsync(id, updateDto);
            if (application == null)
                return NotFound();

            return Ok(application);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteApplication(int id)
        {
            var result = await _jobApplicationService.DeleteApplicationAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<JobApplicationDto>>> GetApplicationsByStatus(ApplicationStatus status)
        {
            var applications = await _jobApplicationService.GetApplicationsByStatusAsync(status);
            return Ok(applications);
        }

        [HttpGet("statistics")]
        public async Task<ActionResult<Dictionary<ApplicationStatus, int>>> GetApplicationStatistics()
        {
            var statistics = await _jobApplicationService.GetApplicationStatisticsAsync();
            return Ok(statistics);
        }
    }
} 