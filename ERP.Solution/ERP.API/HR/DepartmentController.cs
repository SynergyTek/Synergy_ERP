using ERP.HRService.Interfaces;
using ERP.HRService.Models;
using ERP.HRService.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ERP.API.HR
{
    [ApiController]
    [Route("api/v1/department")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _service;
        public DepartmentController(IDepartmentService service) { _service = service; }

        /// <summary>
        /// Returns all departments.
        /// LLM: Use this to list all department records.
        /// Example: GET /api/v1/department
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentViewModel>>> GetAll()
        {
            var departments = await _service.GetAllAsync();
            var result = departments.Select(d => ToViewModel(d));
            return Ok(result);
        }

        /// <summary>
        /// Returns a department by ID.
        /// LLM: Use this to fetch a specific department.
        /// Example: GET /api/v1/department/{id}
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentViewModel>> GetById(string id)
        {
            var department = await _service.GetByIdAsync(id);
            if (department == null) return NotFound();
            return Ok(ToViewModel(department));
        }

        /// <summary>
        /// Creates a new department.
        /// LLM: Use this to add a new department.
        /// Example: POST /api/v1/department { "name": "HR", ... }
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Create(Department department)
        {
            await _service.AddAsync(department);
            return CreatedAtAction(nameof(GetById), new { id = department.Id }, ToViewModel(department));
        }

        /// <summary>
        /// Updates an existing department by ID.
        /// LLM: Use this to modify a department.
        /// Example: PUT /api/v1/department/{id} { "name": "Finance", ... }
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, Department department)
        {
            if (id != department.Id) return BadRequest();
            await _service.UpdateAsync(department);
            return NoContent();
        }

        /// <summary>
        /// Deletes a department by ID.
        /// LLM: Use this to remove a department.
        /// Example: DELETE /api/v1/department/{id}
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        private static DepartmentViewModel ToViewModel(Department d)
        {
            return new DepartmentViewModel
            {
                Id = d.Id,
                Name = d.Name,
                ParentDepartment = d.ParentDepartment == null ? null : new DepartmentViewModel { Id = d.ParentDepartment.Id, Name = d.ParentDepartment.Name },
                ChildDepartments = d.ChildDepartments.Select(cd => new DepartmentViewModel { Id = cd.Id, Name = cd.Name }).ToList()
            };
        }
    }
} 