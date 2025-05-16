using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ERP.Recruitment.DTOs;
using ERP.Recruitment.Interfaces;

namespace ERP.Recruitment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobPositionsController : ControllerBase
    {
        private readonly IJobPositionService _jobPositionService;

        public JobPositionsController(IJobPositionService jobPositionService)
        {
            _jobPositionService = jobPositionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobPositionDto>>> GetAllJobPositions()
        {
            var positions = await _jobPositionService.GetAllJobPositionsAsync();
            return Ok(positions);
        }

        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<JobPositionDto>>> GetActiveJobPositions()
        {
            var positions = await _jobPositionService.GetActiveJobPositionsAsync();
            return Ok(positions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JobPositionDto>> GetJobPosition(int id)
        {
            var position = await _jobPositionService.GetJobPositionByIdAsync(id);
            if (position == null)
                return NotFound();

            return Ok(position);
        }

        [HttpPost]
        public async Task<ActionResult<JobPositionDto>> CreateJobPosition(CreateJobPositionDto createDto)
        {
            var position = await _jobPositionService.CreateJobPositionAsync(createDto);
            return CreatedAtAction(nameof(GetJobPosition), new { id = position.Id }, position);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<JobPositionDto>> UpdateJobPosition(int id, UpdateJobPositionDto updateDto)
        {
            var position = await _jobPositionService.UpdateJobPositionAsync(id, updateDto);
            if (position == null)
                return NotFound();

            return Ok(position);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteJobPosition(int id)
        {
            var result = await _jobPositionService.DeleteJobPositionAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("vacancies/total")]
        public async Task<ActionResult<int>> GetTotalVacancies()
        {
            var total = await _jobPositionService.GetTotalVacanciesAsync();
            return Ok(total);
        }
    }
} 