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

        /// <summary>
        /// Gets All EmployeeContract 
        /// </summary>
        /// <returns>All EmployeeContract Details</returns>
        [HttpGet]
        public async Task<IEnumerable<EmployeeContract>> GetAll() => await _service.GetAllAsync();

        /// <summary>
        /// Gets a specific EmployeeContract by ID.
        /// </summary>
        /// <param name="id">EmployeeContract ID</param>
        /// <returns>EmployeeContract details</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeContract>> GetEmployeeContractById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Creates a new EmployeeContract
        /// </summary>
        /// <remarks>
        /// This endpoint is used during the onboarding workflow. It triggers creation of the EmployeeContract record.
        /// </remarks>
        /// <param name="contract">The EmployeeContract data to create</param>
        /// <returns>Created EmployeeContract ID</returns>
        [HttpPost("CreateEmployeeContract")]
        public async Task<ActionResult<EmployeeContract>> CreateEmployeeContract(EmployeeContract contract)
        {
            var result = await _service.CreateAsync(contract);
            return CreatedAtAction(nameof(GetEmployeeContractById), new { id = result.Id }, result);
        }

        /// <summary>
        /// updates the existing EmployeeContract details
        /// </summary>
        /// <remarks>
        /// This endpoint is used to edit the  existing EmployeeContract records.
        /// </remarks>
        /// <param name="id">The ID of the EmployeeContract to update.</param>
        /// <param name="contract">The EmployeeContract data.</param>
        /// <returns>updated EmployeeContract details</returns>
        [HttpPut("UpdateEmployeeContract/{id}")]
        public async Task<ActionResult<EmployeeContract>> UpdateEmployeeContract(int id, EmployeeContract contract)
        {
            if (id != contract.Id) return BadRequest();
            var result = await _service.UpdateAsync(contract);
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Delete the EmployeeContract
        /// </summary>
        /// <param name="id">EmployeeContract ID</param>
        /// <returns>Deleted EmployeeContract ID</returns>
        [HttpDelete("DeleteEmployeeContract/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
} 