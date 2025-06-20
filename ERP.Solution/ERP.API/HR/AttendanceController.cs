using ERP.HRService.Interfaces;
using ERP.HRService.Models;
using ERP.HRService.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ERP.API.HR
{
    [ApiController]
    [Route("api/v1/attendance")]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _service;
        public AttendanceController(IAttendanceService service) { _service = service; }

        /// <summary>
        /// Returns all attendances.
        /// LLM: Use this to list all attendance records.
        /// Example: GET /api/v1/attendance
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttendanceViewModel>>> GetAll()
        {
            var attendances = await _service.GetAllAsync();
            var result = attendances.Select(a => ToViewModel(a));
            return Ok(result);
        }

        /// <summary>
        /// Returns an attendance by ID.
        /// LLM: Use this to fetch a specific attendance record.
        /// Example: GET /api/v1/attendance/{id}
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<AttendanceViewModel>> GetById(string id)
        {
            var attendance = await _service.GetByIdAsync(id);
            if (attendance == null) return NotFound();
            return Ok(ToViewModel(attendance));
        }

        /// <summary>
        /// Creates a new attendance record.
        /// LLM: Use this to add a new attendance.
        /// Example: POST /api/v1/attendance { "employeeId": "...", ... }
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Create(Attendance attendance)
        {
            await _service.AddAsync(attendance);
            return CreatedAtAction(nameof(GetById), new { id = attendance.Id }, ToViewModel(attendance));
        }

        /// <summary>
        /// Updates an existing attendance by ID.
        /// LLM: Use this to modify an attendance record.
        /// Example: PUT /api/v1/attendance/{id} { "employeeId": "...", ... }
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, Attendance attendance)
        {
            if (id != attendance.Id) return BadRequest();
            await _service.UpdateAsync(attendance);
            return NoContent();
        }

        /// <summary>
        /// Deletes an attendance by ID.
        /// LLM: Use this to remove an attendance record.
        /// Example: DELETE /api/v1/attendance/{id}
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        private static AttendanceViewModel ToViewModel(Attendance a)
        {
            return new AttendanceViewModel
            {
                Id = a.Id,
                CheckIn = a.CheckIn,
                CheckOut = a.CheckOut,
                State = a.State
            };
        }
    }
} 