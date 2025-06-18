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
        /// <summary>
        /// Gets All PayslipLine 
        /// </summary>
        /// <returns>All PayslipLine Details</returns>
        [HttpGet]
        public async Task<IEnumerable<PayslipLine>> GetAll() => await _service.GetAllAsync();


        /// <summary>
        /// Gets a specific PayslipLine by ID.
        /// </summary>
        /// <param name="id">PayslipLine ID</param>
        /// <returns>PayslipLine details</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PayslipLine>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
        /// <summary>
        /// Gets a specific PayslipLine by payslipId.
        /// </summary>
        /// <param name="payslipId"> payslipId</param>
        /// <returns>PayslipLine details</returns>

        [HttpGet("bypayslip/{payslipId}")]
        public async Task<IEnumerable<PayslipLine>> GetByPayslipId(int payslipId) => await _service.GetByPayslipIdAsync(payslipId);


        /// <summary>
        /// Creates a new  PayslipLine
        /// </summary>
        /// <remarks>
        /// This endpoint is used during the onboarding workflow. It triggers creation of the  PayslipLine record.
        /// Example input:
        /// {
        ///   "Name":"John",
        ///   "Amount":"60000",
        ///   "Type":"Earnings",
        ///   "Description":"Monthly Salary",
        ///  }
        /// </remarks>
        /// <param name="line">The  PayslipLine data to create</param>
        /// <returns>Created  PayslipLine ID</returns>
        [HttpPost("CreatePayslipLine")]
        public async Task<ActionResult<PayslipLine>> CreatePayslipLine(PayslipLine line)
        {
            var result = await _service.CreateAsync(line);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Updates the details of an existing payslip Linerecord.
        /// </summary>
        /// <remarks>
        /// This endpoint is used to modify an existing PayslipLine entry based on the provided ID and updated data.
        /// </remarks>
        /// <param name="id">The ID of the PayslipLine to update.</param>
        /// <param name="line">The updated PayslipLine data.</param>
        /// <returns>The updated PayslipLine record.</returns>


        [HttpPut("UpdatePayslipLine/{id}")]
        public async Task<ActionResult<PayslipLine>> UpdatePayslipLine(int id, PayslipLine line)
        {
            if (id != line.Id) return BadRequest();
            var result = await _service.UpdateAsync(line);
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Delete the PayslipLine
        /// </summary>
        /// <param name="id"> PayslipLine ID</param>
        /// <returns>Deleted PayslipLine ID</returns>
        [HttpDelete("DeletePayslipLine/{id}")]
        public async Task<IActionResult> DeletePayslipLine(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
} 