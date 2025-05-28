using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.ViewModels;
using ERP.PayrollService.Interfaces;

namespace ERP.PayrollService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeductionController : ControllerBase
    {
        private readonly IDeductionService _service;
        public DeductionController(IDeductionService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IEnumerable<DeductionViewModel>> GetAll() => await _service.GetAllAsync();
        [HttpGet("{id}")]
        public async Task<ActionResult<DeductionViewModel>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<DeductionViewModel>> Create(DeductionViewModel vm)
        {
            var result = await _service.CreateAsync(vm);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<DeductionViewModel>> Update(int id, DeductionViewModel vm)
        {
            if (id != vm.Id) return BadRequest();
            var result = await _service.UpdateAsync(vm);
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