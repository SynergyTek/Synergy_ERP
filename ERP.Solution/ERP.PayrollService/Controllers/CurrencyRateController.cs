using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.Models;
using ERP.PayrollService.Interfaces;

namespace ERP.PayrollService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyRateController : ControllerBase
    {
        private readonly ICurrencyRateService _service;
        public CurrencyRateController(ICurrencyRateService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IEnumerable<CurrencyRate>> GetAll() => await _service.GetAllAsync();
        [HttpGet("{id}")]
        public async Task<ActionResult<CurrencyRate>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<CurrencyRate>> Create(CurrencyRate rate)
        {
            var result = await _service.CreateAsync(rate);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<CurrencyRate>> Update(int id, CurrencyRate rate)
        {
            if (id != rate.Id) return BadRequest();
            var result = await _service.UpdateAsync(rate);
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