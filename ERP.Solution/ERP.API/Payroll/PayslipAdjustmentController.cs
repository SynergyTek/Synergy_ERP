using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.Models;
using ERP.PayrollService.Interfaces;

namespace ERP.API.HR.Payroll
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
        /// <summary>
        /// Gets All PayslipAdjustment 
        /// </summary>
        /// <returns>All PayslipAdjustment Details</returns>
        [HttpGet]
        public async Task<IEnumerable<PayslipAdjustment>> GetAll() => await _service.GetAllAsync();


        /// <summary>
        /// Gets a specific PayslipAdjustment by ID.
        /// </summary>
        /// <param name="id">PayslipAdjustment ID</param>
        /// <returns>PayslipAdjustment details</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PayslipAdjustment>> GetPayslipAdjustmentById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Creates a new PayslipAdjustment
        /// </summary>
        /// <remarks>
        /// This endpoint is used during the onboarding workflow. It triggers creation of the PayslipAdjustment record.
        /// </remarks>
        /// <param name="adjustment">The PayslipAdjustment data to create</param>
        /// <returns>Created PayslipAdjustment ID</returns>
        [HttpPost("CreatePayslipAdjustment")]
        public async Task<ActionResult<PayslipAdjustment>> CreatePayslipAdjustment(PayslipAdjustment adjustment)
        {
            var result = await _service.CreateAsync(adjustment);
            return CreatedAtAction(nameof(GetPayslipAdjustmentById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Updates the details of an existing payslip adjustment record.
        /// </summary>
        /// <remarks>
        /// This endpoint is used to modify an existing PayslipAdjustment entry based on the provided ID and updated data.
        /// </remarks>
        /// <param name="id">The ID of the PayslipAdjustment to update.</param>
        /// <param name="adjustment">The updated PayslipAdjustment data.</param>
        /// <returns>The updated PayslipAdjustment record.</returns>

        [HttpPut("UpdatePayslipAdjustment/{id}")]
        public async Task<ActionResult<PayslipAdjustment>> UpdatePayslipAdjustment(int id, PayslipAdjustment adjustment)
        {
            if (id != adjustment.Id) return BadRequest();
            var result = await _service.UpdateAsync(adjustment);
            if (result == null) return NotFound();
            return Ok(result);
        }


        /// <summary>
        /// Delete the PayslipAdjustment
        /// </summary>
        /// <param name="id">PayslipAdjustment ID</param>
        /// <returns>Deleted PayslipAdjustment ID</returns>
        [HttpDelete("DeletePayslipAdjustment/{id}")]
        public async Task<IActionResult> DeletePayslipAdjustment(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
} 