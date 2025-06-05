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
        [HttpGet]
        public async Task<IEnumerable<WorkedDayInput>> GetAll() => await _service.GetAllAsync();
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkedDayInput>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<WorkedDayInput>> Create(WorkedDayInput input)
        {
            var result = await _service.CreateAsync(input);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<WorkedDayInput>> Update(int id, WorkedDayInput input)
        {
            if (id != input.Id) return BadRequest();
            var result = await _service.UpdateAsync(input);
            if (result == null) return NotFound();
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
} 