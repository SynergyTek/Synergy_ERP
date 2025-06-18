using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.Models;
using ERP.PayrollService.Interfaces;

namespace ERP.PayrollService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _service;
        public LoanController(ILoanService service)
        {
            _service = service;
        }
        /// <summary>
        /// Gets All Loan details 
        /// </summary>
        /// <returns>All Loan Details</returns>
        [HttpGet]
        public async Task<IEnumerable<Loan>> GetAll() => await _service.GetAllAsync();

        /// <summary>
        /// Gets a specific Loan by ID.
        /// </summary>
        /// <param name="id">Loan ID</param>
        /// <returns>Loan details</returns>

        [HttpGet("{id}")]
        public async Task<ActionResult<Loan>> GetLoanById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Creates a new Loan
        /// </summary>
        /// <remarks>
        /// This endpoint is used during the onboarding workflow. It triggers creation of the Loan record.
        /// </remarks>
        /// <param name="loan">The Loan data to create</param>
        /// <returns>Created Loan ID</returns>
        [HttpPost("CreateLoan")]
        public async Task<ActionResult<Loan>> CreateLoan(Loan loan)
        {
            var result = await _service.CreateAsync(loan);
            return CreatedAtAction(nameof(GetLoanById), new { id = result.Id }, result);
        }

        /// <summary>
        /// updates the existing Loan details
        /// </summary>
        /// <remarks>
        /// This endpoint is used to edit the  existing Loan records.
        /// </remarks>
        /// <param name="id">Loan ID</param>
        /// <param name="loan">Loan data </param>
        /// <returns>updated Loan details</returns>

        [HttpPut("UpdateLoan/{id}")]
        public async Task<ActionResult<Loan>> UpdateLoan(int id, Loan loan)
        {
            if (id != loan.Id) return BadRequest();
            var result = await _service.UpdateAsync(loan);
            if (result == null) return NotFound();
            return Ok(result);
        }


        /// <summary>
        /// Delete the Loan
        /// </summary>
        /// <param name="id">Loan ID</param>
        /// <returns>Deleted Loan ID</returns>
        [HttpDelete("DeleteLoan/{id}")]
        public async Task<IActionResult> DeleteLoan(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
} 