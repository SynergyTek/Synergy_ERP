using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.Models;
using ERP.PayrollService.Interfaces;

namespace ERP.PayrollService.Controllers
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
        [HttpGet]
        public async Task<IEnumerable<RetroactiveChangeLog>> GetAll() => await _service.GetAllAsync();
        [HttpGet("{id}")]
        public async Task<ActionResult<RetroactiveChangeLog>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<RetroactiveChangeLog>> Create(RetroactiveChangeLog log)
        {
            var result = await _service.CreateAsync(log);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<RetroactiveChangeLog>> Update(int id, RetroactiveChangeLog log)
        {
            if (id != log.Id) return BadRequest();
            var result = await _service.UpdateAsync(log);
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