using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.Models;
using ERP.PayrollService.Interfaces;

namespace ERP.PayrollService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeContractController : ControllerBase
    {
        private readonly IEmployeeContractService _service;
        public EmployeeContractController(IEmployeeContractService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IEnumerable<EmployeeContract>> GetAll() => await _service.GetAllAsync();
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeContract>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<EmployeeContract>> Create(EmployeeContract contract)
        {
            var result = await _service.CreateAsync(contract);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeContract>> Update(int id, EmployeeContract contract)
        {
            if (id != contract.Id) return BadRequest();
            var result = await _service.UpdateAsync(contract);
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