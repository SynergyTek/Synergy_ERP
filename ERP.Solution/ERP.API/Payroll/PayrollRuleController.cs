using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.ViewModels;
using ERP.PayrollService.Interfaces;

namespace ERP.API.HR.Payroll
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayrollRuleController : ControllerBase
    {
        private readonly IPayrollRuleService _service;
        public PayrollRuleController(IPayrollRuleService service)
        {
            _service = service;
        }
        /// <summary>
        /// Gets All PayrollRule 
        /// </summary>
        /// <returns>All PayrollRule Details</returns>
        [HttpGet]
        public async Task<IEnumerable<PayrollRuleViewModel>> GetAll() => await _service.GetAllAsync();


        /// <summary>
        /// Gets a specific PayrollRule by ID.
        /// </summary>
        /// <param name="id">PayrollRule ID</param>
        /// <returns>PayrollRule details</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PayrollRuleViewModel>> GetPayrollRuleById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
        /// <summary>
        /// Creates a new PayrollRule
        /// </summary>
        /// <remarks>
        /// This endpoint is used during the onboarding workflow. It triggers creation of the PayrollRule record.
        /// Example input:
        /// {
        ///   "Name":"Payroll 1",
        ///   "Formula":"BaseSalary * 0.10 + 100",
        ///   "Sequence":"1"
        /// }
        /// </remarks>
        /// <param name="vm">The PayrollRule data to create</param>
        /// <returns>Created PayrollRule ID</returns>
        [HttpPost("CreatePayrollRule")]
        public async Task<ActionResult<PayrollRuleViewModel>> CreatePayrollRule(PayrollRuleViewModel vm)
        {
            var result = await _service.CreateAsync(vm);
            return CreatedAtAction(nameof(GetPayrollRuleById), new { id = result.Id }, result);
        }


        /// <summary>
        /// Updates the details of an existing payroll rule record.
        /// </summary>
        /// <remarks>
        /// This endpoint is used to modify an existing PayrollRule based on the provided ID and updated data.
        /// </remarks>
        /// <param name="id">The ID of the payroll rule to update.</param>
        /// <param name="vm">The updated PayrollRule data.</param>
        /// <returns>The updated PayrollRule record</returns>

        [HttpPut("UpdatePayrollRule/{id}")]
        public async Task<ActionResult<PayrollRuleViewModel>>UpdatePayrollRule(int id, PayrollRuleViewModel vm)
        {
            if (id != vm.Id) return BadRequest();
            var result = await _service.UpdateAsync(vm);
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Delete the PayrollRule
        /// </summary>
        /// <param name="id">PayrollRule ID</param>
        /// <returns>Deleted PayrollRule ID</returns>
        [HttpDelete("DeletePayrollRule/{id}")]
        public async Task<IActionResult> DeletePayrollRule(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
} 