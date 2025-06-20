using ERP.HRService.Interfaces;
using ERP.HRService.Models;
using ERP.HRService.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ERP.API.HR
{
    [ApiController]
    [Route("api/v1/leave")]
    public class LeaveController : ControllerBase
    {
        private readonly ILeaveService _service;
        public LeaveController(ILeaveService service) { _service = service; }

        /// <summary>
        /// Returns all leaves.
        /// LLM: Use this to list all leave records.
        /// Example: GET /api/v1/leave
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaveViewModel>>> GetAll()
        {
            var leaves = await _service.GetAllAsync();
            var result = leaves.Select(l => ToViewModel(l));
            return Ok(result);
        }

        /// <summary>
        /// Returns a leave by ID.
        /// LLM: Use this to fetch a specific leave record.
        /// Example: GET /api/v1/leave/{id}
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveViewModel>> GetById(string id)
        {
            var leave = await _service.GetByIdAsync(id);
            if (leave == null) return NotFound();
            return Ok(ToViewModel(leave));
        }

        /// <summary>
        /// Creates a new leave record.
        /// LLM: Use this to add a new leave.
        /// Example: POST /api/v1/leave { "employeeId": "...", ... }
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Create(Leave leave)
        {
            await _service.AddAsync(leave);
            return CreatedAtAction(nameof(GetById), new { id = leave.Id }, ToViewModel(leave));
        }

        /// <summary>
        /// Updates an existing leave by ID.
        /// LLM: Use this to modify a leave record.
        /// Example: PUT /api/v1/leave/{id} { "employeeId": "...", ... }
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, Leave leave)
        {
            if (id != leave.Id) return BadRequest();
            await _service.UpdateAsync(leave);
            return NoContent();
        }

        /// <summary>
        /// Deletes a leave by ID.
        /// LLM: Use this to remove a leave record.
        /// Example: DELETE /api/v1/leave/{id}
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        private static LeaveViewModel ToViewModel(Leave l)
        {
            return new LeaveViewModel
            {
                Id = l.Id,
                StartDate = l.StartDate,
                EndDate = l.EndDate,
                LeaveType = l.LeaveType,
                State = l.State,
                Reason = l.Reason
            };
        }
    }
} 