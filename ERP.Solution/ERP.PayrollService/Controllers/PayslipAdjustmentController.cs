using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.Models;
using ERP.PayrollService.Interfaces;

namespace ERP.PayrollService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayslipAdjustmentController : ControllerBase
    {
        private readonly IPayslipAdjustmentService _service;
        public PayslipAdjustmentController(IPayslipAdjustmentService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IEnumerable<PayslipAdjustment>> GetAll() => await _service.GetAllAsync();
        [HttpGet("{id}")]
        public async Task<ActionResult<PayslipAdjustment>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<PayslipAdjustment>> Create(PayslipAdjustment adjustment)
        {
            var result = await _service.CreateAsync(adjustment);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<PayslipAdjustment>> Update(int id, PayslipAdjustment adjustment)
        {
            if (id != adjustment.Id) return BadRequest();
            var result = await _service.UpdateAsync(adjustment);
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