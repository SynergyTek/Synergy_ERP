using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.Models;
using ERP.PayrollService.Interfaces;

namespace ERP.PayrollService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocalizationInfoController : ControllerBase
    {
        private readonly ILocalizationInfoService _service;
        public LocalizationInfoController(ILocalizationInfoService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IEnumerable<LocalizationInfo>> GetAll() => await _service.GetAllAsync();
        [HttpGet("{id}")]
        public async Task<ActionResult<LocalizationInfo>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<LocalizationInfo>> Create(LocalizationInfo info)
        {
            var result = await _service.CreateAsync(info);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<LocalizationInfo>> Update(int id, LocalizationInfo info)
        {
            if (id != info.Id) return BadRequest();
            var result = await _service.UpdateAsync(info);
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