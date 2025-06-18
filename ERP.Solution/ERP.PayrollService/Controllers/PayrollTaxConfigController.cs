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

        /// <summary>
        /// Gets All PayrollTaxConfig 
        /// </summary>
        /// <returns>All PayrollTaxConfig Details</returns>
        [HttpGet]
        public async Task<IEnumerable<PayrollTaxConfigViewModel>> GetAll() => await _service.GetAllAsync();

        /// <summary>
        /// Gets a specific PayrollTaxConfig by ID.
        /// </summary>
        /// <param name="id">PayrollTaxConfig ID</param>
        /// <returns>PayrollTaxConfig details</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PayrollTaxConfigViewModel>> GetPayrollTaxConfigById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Creates a new PayrollTaxConfig
        /// </summary>
        /// <remarks>
        /// This endpoint is used during the onboarding workflow. It triggers creation of the PayrollTaxConfig record.
        /// Example input:
        /// {
        ///  "Name": "Professional Tax",
        ///  "Rate": 2.5,
        ///  "Threshold": 5000,
        /// }
        /// </remarks>
        /// <param name="ViewModel">The PayrollTaxConfig data to create</param>
        /// <returns>Created PayrollTaxConfig ID</returns>
        [HttpPost("CreatePayrollTaxConfig")]
        public async Task<ActionResult<PayrollTaxConfigViewModel>> CreatePayrollTaxConfig(PayrollTaxConfigViewModel ViewModel)
        {
            var result = await _service.CreateAsync(ViewModel);
            return CreatedAtAction(nameof(GetPayrollTaxConfigById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Updates the details of an existing payroll tax configuration record.
        /// </summary>
        /// <remarks>
        /// This endpoint is used to modify an existing PayrollTaxConfig entry based on the provided ID and updated data.
        /// </remarks>
        /// <param name="id">The ID of the PayrollTaxConfig to update.</param>
        /// <param name="ViewModel">The updated PayrollTaxConfig data.</param>
        /// <returns>The updated PayrollTaxConfig record.</returns>

        [HttpPut("UpdatePayrollTaxConfig/{id}")]
        public async Task<ActionResult<PayrollTaxConfigViewModel>> UpdatePayrollTaxConfig(int id, PayrollTaxConfigViewModel ViewModel)
        {
            if (id != ViewModel.Id) return BadRequest();
            var result = await _service.UpdateAsync(ViewModel);
            if (result == null) return NotFound();
            return Ok(result);
        }
        /// <summary>
        /// Delete the PayrollTaxConfig
        /// </summary>
        /// <param name="id">PayrollTaxConfig ID</param>
        /// <returns>Deleted PayrollTaxConfig ID</returns>
        [HttpDelete("DeletePayrollTaxConfig/{id}")]
        public async Task<IActionResult> DeletePayrollTaxConfig(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
} 