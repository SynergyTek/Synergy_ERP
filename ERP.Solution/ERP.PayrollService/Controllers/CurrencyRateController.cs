using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.Models;
using ERP.PayrollService.Interfaces;

namespace ERP.PayrollService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyRateController : ControllerBase
    {
        private readonly ICurrencyRateService _service;
        public CurrencyRateController(ICurrencyRateService service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets All CurrencyRate 
        /// </summary>
        /// <returns>All CurrencyRate Details</returns>
        [HttpGet]
        public async Task<IEnumerable<CurrencyRate>> GetAll() => await _service.GetAllAsync();

        /// <summary>
        /// Gets a specific CurrencyRate by ID.
        /// </summary>
        /// <param name="id">CurrencyRate ID</param>
        /// <returns>CurrencyRate details</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CurrencyRate>> GetCurrencyRateById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }


        /// <summary>
        /// Creates a new CurrencyRate
        /// </summary>
        /// <remarks>
        /// This endpoint is used during the onboarding workflow. It triggers creation of the CurrencyRate record.
        /// </remarks>
        /// <param name="rate">The CurrencyRate data to create</param>
        /// <returns>Created CurrencyRate ID</returns>
        [HttpPost("CreateCurrencyRate")]
        public async Task<ActionResult<CurrencyRate>> CreateCurrencyRate(CurrencyRate rate)
        {
            var result = await _service.CreateAsync(rate);
            return CreatedAtAction(nameof(GetCurrencyRateById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Updates the details of an existing currency rate record.
        /// </summary>
        /// <remarks>
        /// This endpoint is used to update an existing CurrencyRate entry based on its ID.
        /// </remarks>
        /// <param name="id">The ID of the currency rate to update.</param>
        /// <param name="rate">The currency rate data to update.</param>
        /// <returns>The updated CurrencyRate record, or 404 if not found.</returns>

        public async Task<ActionResult<CurrencyRate>> UpdateCurrencyRate(int id, CurrencyRate rate)
        {
            if (id != rate.Id) return BadRequest();
            var result = await _service.UpdateAsync(rate);
            if (result == null) return NotFound();
            return Ok(result);
        }
        /// <summary>
        /// Delete the CurrencyRate
        /// </summary>
        /// <param name="id">CurrencyRate ID</param>
        /// <returns>Delete CurrencyRate ID</returns>
        [HttpDelete("DeleteCurrencyRate/{id}")]
        public async Task<IActionResult> DeleteCurrencyRate(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
} 