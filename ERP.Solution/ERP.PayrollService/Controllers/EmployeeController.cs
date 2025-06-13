using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.ViewModels;
using ERP.PayrollService.Interfaces;

namespace ERP.PayrollService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;
        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IEnumerable<EmployeeViewModel>> GetAll() => await _service.GetAllAsync();

        /// <summary>
        /// Gets a specific employee by ID.
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <returns>Employee details</returns>

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeViewModel>> GetEmployeeById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
        /// <summary>
        /// Creates a new employee 
        /// </summary>
        /// <remarks>
        /// This endpoint is used during the onboarding workflow. It triggers creation of the employee record,
        /// assigns a default employment contract, and configures their leave quota.
        /// 
        /// **Workflow**: employee_onboarding  
        /// **Related APIs**: POST /api/contracts, POST /api/leaves
        /// 
        /// Example input:
        /// {
        ///   "Name": "John Doe",
        ///   "Email": "john.doe@example.com",
        ///   "Designation": "Software Engineer",
        ///   "BaseSalary": 50000,
        ///   "Position": "Software Engineer"
        /// }
        /// </remarks>
        /// <param name="request">The employee data to create</param>
        /// <returns>Created employee's ID</returns>

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<EmployeeViewModel>> CreateEmployee(EmployeeViewModel vm)
        {
            var result = await _service.CreateAsync(vm);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = result.Id }, result);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeViewModel>> Update(int id, EmployeeViewModel vm)
        {
            if (id != vm.Id) return BadRequest();
            var result = await _service.UpdateAsync(vm);
            if (result == null) return NotFound();
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
} 