using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.Models;
using ERP.PayrollService.Interfaces;
using ERP.PayrollService.ViewModels;

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
        /// <summary>
        /// Gets All Deduction
        /// </summary>
        /// <returns>All  Deduction Details</returns>
        [HttpGet]
        public async Task<IEnumerable<DeductionViewModel>> GetAll() => await _service.GetAllAsync();

        /// <summary>
        /// Gets a specific  Deduction by ID.
        /// </summary>
        /// <param name="id"> Deduction ID</param>
        /// <returns> Deduction details</returns>

        [HttpGet("{id}")]
        public async Task<ActionResult<DeductionViewModel>> GetDeductionById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Creates a new  Deduction
        /// </summary>
        /// <remarks>
        /// This endpoint is used during the onboarding workflow. It triggers creation of the  Deduction record.
        ///{
        ///   "Name":"John",
        ///   "Amount":"500"
        /// }
        /// </remarks>
        /// <param name="deduction">The  Deduction data to create</param>
        /// <returns>Created  Deduction ID</returns>
        [HttpPost("CreateDeduction")]
        public async Task<ActionResult<DeductionViewModel>> CreateDeduction(DeductionViewModel deduction)
        {
            var result = await _service.CreateAsync(deduction);
            return CreatedAtAction(nameof(GetDeductionById), new { id = result.Id }, result);
        }
        /// <summary>
        /// updates the existing  Deduction details
        /// </summary>
        /// <remarks>
        /// This endpoint is used to edit the  existing  Deduction records.
        /// </remarks>
        /// <param name="id">The ID of the deduction to update</param>
        /// <param name="deduction">The deduction data to update</param>
        /// <returns>updated  Deduction details</returns>
        [HttpPut("UpdateDeduction/{id}")]
        public async Task<ActionResult<DeductionViewModel>> UpdateDeduction(int id, DeductionViewModel deduction)
        {
            if (id != deduction.Id) return BadRequest();
            var result = await _service.UpdateAsync(deduction);
            if (result == null) return NotFound();
            return Ok(result);
        }
        /// <summary>
        /// Delete the  Deduction
        /// </summary>
        /// <param name="id"> Deduction ID</param>
        /// <returns>Deleted Deduction ID</returns>
        [HttpDelete("DeleteDeduction/{id}")]
        public async Task<IActionResult> DeleteDeduction(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
} 