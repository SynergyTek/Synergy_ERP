using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.Models;
using ERP.PayrollService.Interfaces;

namespace ERP.API.HR.Payroll
{
    [ApiController]
    [Route("api/[controller]")]
    public class RetroactiveChangeLogController : ControllerBase
    {
        private readonly IRetroactiveChangeLogService _service;
        public RetroactiveChangeLogController(IRetroactiveChangeLogService service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets All  RetroactiveChangeLog 
        /// </summary>
        /// <returns>All  RetroactiveChangeLog Details</returns>
        [HttpGet]
        public async Task<IEnumerable<RetroactiveChangeLog>> GetAll() => await _service.GetAllAsync();

        /// <summary>
        /// Gets a specific  RetroactiveChangeLog by ID.
        /// </summary>
        /// <param name="id"> RetroactiveChangeLog ID</param>
        /// <returns> RetroactiveChangeLog details</returns>

        [HttpGet("{id}")]
        public async Task<ActionResult<RetroactiveChangeLog>> GetRetroactiveChangeLogById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Creates a new  RetroactiveChangeLog
        /// </summary>
        /// <remarks>
        /// This endpoint is used during the onboarding workflow. It triggers creation of the  RetroactiveChangeLog record.
        /// </remarks>
        /// <param name="log">The  RetroactiveChangeLog data to create</param>
        /// <returns>Created  RetroactiveChangeLog ID</returns>
        [HttpPost("CreateRetroactiveChangeLog")]
        public async Task<ActionResult<RetroactiveChangeLog>> CreateRetroactiveChangeLog(RetroactiveChangeLog log)
        {
            var result = await _service.CreateAsync(log);
            return CreatedAtAction(nameof(GetRetroactiveChangeLogById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Updates the details of an existing RetroactiveChangeLog record.
        /// </summary>
        /// <remarks>
        /// This endpoint is used to modify an existing RetroactiveChangeLog entry based on the provided ID and updated data.
        /// </remarks>
        /// <param name="id">The ID of the RetroactiveChangeLog to update.</param>
        /// <param name="log">The updated RetroactiveChangeLog data.</param>
        /// <returns>The updated RetroactiveChangeLog record.</returns>


        [HttpPut("UpdateRetroactiveChangeLog/{id}")]
        public async Task<ActionResult<RetroactiveChangeLog>> UpdateRetroactiveChangeLog(int id, RetroactiveChangeLog log)
        {
            if (id != log.Id) return BadRequest();
            var result = await _service.UpdateAsync(log);
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Delete the RetroactiveChangeLog
        /// </summary>
        /// <param name="id">RetroactiveChangeLog ID</param>
        /// <returns>Deleted RetroactiveChangeLog ID</returns>
        [HttpDelete("DeleteRetroactiveChangeLog/{id}")]
        public async Task<IActionResult> DeleteRetroactiveChangeLog(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
} 