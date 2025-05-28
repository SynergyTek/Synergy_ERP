using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.HRService.Recruitment.Data;
using ERP.HRService.Recruitment.Models;
using Synergy.Business.Interface;

namespace ERP.HRService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobApplicationController(IContextBase<JobApplication, RecruitmentDbContext> context) : ControllerBase
    {
        // GET: api/JobApplication
        [HttpGet]
        public async Task<IEnumerable<JobApplication>?> GetJobApplications()
        {
            return await context.GetAllAsync();
        }

        // GET: api/JobApplication/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobApplication>> GetJobApplication(Guid id)
        {
            var jobApplication = await context.GetByIdAsync(id);

            if (jobApplication == null)
            {
                return NotFound();
            }

            return jobApplication;
        }

        // PUT: api/JobApplication/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobApplication(Guid id, JobApplication jobApplication)
        {
            if (id != jobApplication.Id)
            {
                return BadRequest();
            }

            jobApplication.Status = ApplicationStatus.New;

            await context.UpdateAsync(jobApplication);
            return Ok();
        }

        // POST: api/JobApplication
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JobApplication>> PostJobApplication(JobApplication jobApplication)
        {
            var model = await context.AddAsync(jobApplication);
            return CreatedAtAction("GetJobApplication", new { id = model?.Id });
        }

        // DELETE: api/JobApplication/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobApplication(Guid id)
        {
            var jobApplication = await context.GetByIdAsync(id);
            if (jobApplication == null)
            {
                return NotFound();
            }

            await context.DeleteAsync(id);
            return NoContent();
        }

    }
}