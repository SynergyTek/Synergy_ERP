using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.ViewModels;
using ERP.PayrollService.Interfaces;

namespace ERP.PayrollService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AllowanceController : ControllerBase
    {
        private readonly IAllowanceService _service;
        public AllowanceController(IAllowanceService service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets All Allowance 
        /// </summary>
        /// <returns>All Allowance Details</returns>
        [HttpGet]
        public async Task<IEnumerable<AllowanceViewModel>> GetAll() => await _service.GetAllAsync();


        /// <summary>
        /// Gets a specific Allowance by ID.
        /// </summary>
        /// <param name="id">Allowance ID</param>
        /// <returns>Allowance details</returns>

        [HttpGet("{id}")]
        public async Task<ActionResult<AllowanceViewModel>> GetAllowanceById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }


        /// <summary>
        /// Creates a new Allowance
        /// </summary>
        /// <remarks>
        /// This endpoint is used during the onboarding workflow. It triggers creation of the Allowance record. 
        /// Example input:
        ///   {
        ///   "Name":"John",
        ///   "Amount":"50000"
        ///   }
        /// </remarks>
        /// <param name="vm">The Allowance data to create</param>
        /// <returns>Created Allowance ID</returns>
        [HttpPost("CreateAllowance")]
        public async Task<ActionResult<AllowanceViewModel>> CreateAllowance(AllowanceViewModel vm)
        {
            var result = await _service.CreateAsync(vm);
            return CreatedAtAction(nameof(GetAllowanceById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Updates the details of an existing allowance record.
        /// </summary>
        /// <remarks>
        /// This endpoint is used to modify an existing allowance entry based on its ID.
        /// </remarks>
        /// <param name="id">The ID of the allowance to update.</param>
        /// <param name="vm">The allowance data to update.</param>
        /// <returns>The updated allowance record</returns>
        [HttpPut("updateallowance/{id}")]
        public async Task<ActionResult<AllowanceViewModel>> UpdateAllowance(int id, AllowanceViewModel vm)
        {
            if (id != vm.Id) return BadRequest();
            var result = await _service.UpdateAsync(vm);
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Delete the Allowance
        /// </summary>
        /// <param name="id">Allowance ID</param>
        /// <returns>Delete Allowance ID</returns>
        [HttpDelete("deleteallowance/{id}")]
        public async Task<IActionResult> DeleteAllowance(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
} 