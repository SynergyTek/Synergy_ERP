using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.Models;
using ERP.PayrollService.Interfaces;

namespace ERP.PayrollService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkedDayInputController : ControllerBase
    {
        private readonly IWorkedDayInputService _service;
        public WorkedDayInputController(IWorkedDayInputService service)
        {
            _service = service;
        }
        /// <summary>
        /// Gets All WorkedDayInput  
        /// </summary>
        /// <returns>All WorkedDayInput Details</returns>

        [HttpGet]
        public async Task<IEnumerable<WorkedDayInput>> GetAll() => await _service.GetAllAsync();

        /// <summary>
        /// Gets a specific WorkedDayInput by ID.
        /// </summary>
        /// <param name="id">WorkedDayInput ID</param>
        /// <returns>WorkedDayInput details</returns>

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkedDayInput>> GetWorkedDayInputById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Creates a new WorkedDayInput
        /// </summary>
        /// <remarks>
        /// This endpoint is used during the onboarding workflow. It triggers creation of the WorkedDayInput record.
        /// </remarks>
        /// <param name="input">The WorkedDayInput data to create</param>
        /// <returns>Created WorkedDayInput ID</returns>
        [HttpPost("CreateWorkedDayInput")]
        public async Task<ActionResult<WorkedDayInput>> CreateWorkedDayInput(WorkedDayInput input)
        {
            var result = await _service.CreateAsync(input);
            return CreatedAtAction(nameof(GetWorkedDayInputById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Updates the details of an existing WorkedDayInput record.
        /// </summary>
        /// <remarks>
        /// This endpoint is used to modify an existing WorkedDayInput entry based on the provided ID and updated data.
        /// </remarks>
        /// <param name="id">The ID of the WorkedDayInput to update.</param>
        /// <param name="input">The updated WorkedDayInput data.</param>
        /// <returns>The updated WorkedDayInput record.</returns>
        [HttpPut(" UpdateWorkedDayInput/{id}")]
        public async Task<ActionResult<WorkedDayInput>> UpdateWorkedDayInput(int id, WorkedDayInput input)
        {
            if (id != input.Id) return BadRequest();
            var result = await _service.UpdateAsync(input);
            if (result == null) return NotFound();
            return Ok(result);
        }
        /// <summary>
        /// Delete the WorkedDayInput
        /// </summary>
        /// <param name="id">The ID of the WorkedDayInput to delete</param>
        /// <returns>Deleted WorkedDayInput ID</returns>
        [HttpDelete("DeleteWorkedDayInput/{id}")]
        public async Task<IActionResult> DeleteWorkedDayInput(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
} 