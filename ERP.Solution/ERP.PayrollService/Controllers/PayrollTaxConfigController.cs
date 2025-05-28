using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.ViewModels;
using ERP.PayrollService.Interfaces;

namespace ERP.PayrollService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayrollTaxConfigController : ControllerBase
    {
        private readonly IPayrollTaxConfigService _service;
        public PayrollTaxConfigController(IPayrollTaxConfigService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IEnumerable<PayrollTaxConfigViewModel>> GetAll() => await _service.GetAllAsync();
        [HttpGet("{id}")]
        public async Task<ActionResult<PayrollTaxConfigViewModel>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<PayrollTaxConfigViewModel>> Create(PayrollTaxConfigViewModel ViewModel)
        {
            var result = await _service.CreateAsync(ViewModel);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<PayrollTaxConfigViewModel>> Update(int id, PayrollTaxConfigViewModel ViewModel)
        {
            if (id != ViewModel.Id) return BadRequest();
            var result = await _service.UpdateAsync(ViewModel);
            if (result == null) return NotFound();
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
} 