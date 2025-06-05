using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.Models;
using ERP.PayrollService.Interfaces;

namespace ERP.PayrollService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayslipLineController : ControllerBase
    {
        private readonly IPayslipLineService _service;
        public PayslipLineController(IPayslipLineService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IEnumerable<PayslipLine>> GetAll() => await _service.GetAllAsync();
        [HttpGet("{id}")]
        public async Task<ActionResult<PayslipLine>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
        [HttpGet("bypayslip/{payslipId}")]
        public async Task<IEnumerable<PayslipLine>> GetByPayslipId(int payslipId) => await _service.GetByPayslipIdAsync(payslipId);
        [HttpPost]
        public async Task<ActionResult<PayslipLine>> Create(PayslipLine line)
        {
            var result = await _service.CreateAsync(line);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<PayslipLine>> Update(int id, PayslipLine line)
        {
            if (id != line.Id) return BadRequest();
            var result = await _service.UpdateAsync(line);
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