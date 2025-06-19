using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.Models;
using ERP.PayrollService.Interfaces;

namespace ERP.PayrollService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocalizationInfoController : ControllerBase
    {
        private readonly ILocalizationInfoService _service;
        public LocalizationInfoController(ILocalizationInfoService service)
        {
            _service = service;
        }
        /// <summary>
        /// Gets All  LocalizationInfo 
        /// </summary>
        /// <returns>All  LocalizationInfo Details</returns>
        [HttpGet]
        public async Task<IEnumerable<LocalizationInfo>> GetAll() => await _service.GetAllAsync();

        /// <summary>
        /// Gets a specific  LocalizationInfo by ID.
        /// </summary>
        /// <param name="id"> LocalizationInfo ID</param>
        /// <returns> LocalizationInfo details</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<LocalizationInfo>> GetLocalizationInfoById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }


        /// <summary>
        /// Creates a new  LocalizationInfo
        /// </summary>
        /// <remarks>
        /// This endpoint is used during the onboarding workflow. It triggers creation of the  LocalizationInfo record.
        /// </remarks>
        /// <param name="info">The  LocalizationInfo data to create</param>
        /// <returns>Created  LocalizationInfo ID</returns>
        [HttpPost("CreateLocalizationInfo")]
        public async Task<ActionResult<LocalizationInfo>> CreateLocalizationInfo(LocalizationInfo info)
        {
            var result = await _service.CreateAsync(info);
            return CreatedAtAction(nameof(GetLocalizationInfoById), new { id = result.Id }, result);
        }


        /// <summary>
        /// updates the existing  LocalizationInfo details
        /// </summary>
        /// <remarks>
        /// This endpoint is used to edit the  existing  LocalizationInfo records.
        /// </remarks>
        /// <param name="id"> LocalizationInfo ID</param>
        /// <param name="info"> LocalizationInfo data</param>
        /// <returns>updated LocalizationInfo details</returns>
        [HttpPut("UpdateLocalizationInfo/{id}")]
        public async Task<ActionResult<LocalizationInfo>> UpdateLocalizationInfo(int id, LocalizationInfo info)
        {
            if (id != info.Id) return BadRequest();
            var result = await _service.UpdateAsync(info);
            if (result == null) return NotFound();
            return Ok(result);
        }
        /// <summary>
        /// Delete the  LocalizationInfo 
        /// </summary>
        /// <param name="id"> LocalizationInfo ID</param>
        /// <returns>Deleted LocalizationInfo ID</returns>
        [HttpDelete("DeleteLocalizationInfo/{id}")]
        public async Task<IActionResult> DeleteLocalizationInfo(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
} 