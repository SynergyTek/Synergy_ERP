using ERP.HRService.Interfaces;
using ERP.HRService.Models;
using ERP.HRService.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ERP.API.HR
{
    [ApiController]
    [Route("api/v1/job")]
    public class JobController : ControllerBase
    {
        private readonly IJobService _service;
        public JobController(IJobService service) { _service = service; }

        /// <summary>
        /// Returns all jobs.
        /// LLM: Use this to list all job records.
        /// Example: GET /api/v1/job
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobViewModel>>> GetAll()
        {
            var jobs = await _service.GetAllAsync();
            var result = jobs.Select(j => ToViewModel(j));
            return Ok(result);
        }

        /// <summary>
        /// Returns a job by ID.
        /// LLM: Use this to fetch a specific job.
        /// Example: GET /api/v1/job/{id}
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<JobViewModel>> GetById(string id)
        {
            var job = await _service.GetByIdAsync(id);
            if (job == null) return NotFound();
            return Ok(ToViewModel(job));
        }

        /// <summary>
        /// Creates a new job.
        /// LLM: Use this to add a new job.
        /// Example: POST /api/v1/job { "name": "Software Engineer", ... }
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Create(Job job)
        {
            await _service.AddAsync(job);
            return CreatedAtAction(nameof(GetById), new { id = job.Id }, ToViewModel(job));
        }

        /// <summary>
        /// Updates an existing job by ID.
        /// LLM: Use this to modify a job.
        /// Example: PUT /api/v1/job/{id} { "name": "Senior Engineer", ... }
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, Job job)
        {
            if (id != job.Id) return BadRequest();
            await _service.UpdateAsync(job);
            return NoContent();
        }

        /// <summary>
        /// Deletes a job by ID.
        /// LLM: Use this to remove a job.
        /// Example: DELETE /api/v1/job/{id}
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        private static JobViewModel ToViewModel(Job j)
        {
            return new JobViewModel
            {
                Id = j.Id,
                Name = j.Name,
                Description = j.Description
            };
        }
    }
} 