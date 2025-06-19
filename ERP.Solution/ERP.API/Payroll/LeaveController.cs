using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.Models;
using ERP.PayrollService.Interfaces;

namespace ERP.API.HR.Payroll
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaveController : ControllerBase
    {
        private readonly ILeaveService _service;
        public LeaveController(ILeaveService service)
        {
            _service = service;
        }
        /// <summary>
        /// Gets All Leave details
        /// </summary>
        /// <returns>All Leave Details</returns>
        [HttpGet]
        public async Task<IEnumerable<Leave>> GetAll() => await _service.GetAllAsync();


        /// <summary>
        /// Gets a specific Leave by ID.
        /// </summary>
        /// <param name="id">Leave ID</param>
        /// <returns>Leave details</returns>

        [HttpGet("{id}")]
        public async Task<ActionResult<Leave>> GetLeaveById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
        /// <summary>
        /// Creates a new Leave
        /// </summary>
        /// <remarks>
        /// This endpoint is used during the onboarding workflow. It triggers creation of the Leave record.
        /// </remarks>
        /// <param name="leave">The Leave data to create</param>
        /// <returns>Created Leave ID</returns>
        [HttpPost("CreateLeave")]
        public async Task<ActionResult<Leave>> CreateLeave(Leave leave)
        {
            var result = await _service.CreateAsync(leave);
            return CreatedAtAction(nameof(GetLeaveById), new { id = result.Id }, result);
        }

        /// <summary>
        /// updates the existing Leave details
        /// </summary>
        /// <remarks>
        /// This endpoint is used to edit the  existing Leave records.
        /// </remarks>
        /// <param name="id">Leave ID</param>
        /// <param name="leave">Leave data </param>
        /// <returns>updated Leave details</returns>
        [HttpPut("UpdateLeave/{id}")]
        public async Task<ActionResult<Leave>> UpdateLeave(int id, Leave leave)
        {
            if (id != leave.Id) return BadRequest();
            var result = await _service.UpdateAsync(leave);
            if (result == null) return NotFound();
            return Ok(result);
        }
        /// <summary>
        /// Delete the Leave id
        /// </summary>
        /// <param name="id">Leave ID</param>
        /// <returns>Deleted Leave ID</returns>
        [HttpDelete("DeleteLeave/{id}")]
        public async Task<IActionResult> DeleteLeave(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
} 