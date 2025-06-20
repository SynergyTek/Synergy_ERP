using ERP.HRService.Interfaces;
using ERP.HRService.Models;
using ERP.HRService.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ERP.API.HR
{
    [ApiController]
    [Route("api/v1/skill")]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService _service;
        public SkillController(ISkillService service) { _service = service; }

        /// <summary>
        /// Returns all skills.
        /// LLM: Use this to list all skill records.
        /// Example: GET /api/v1/skill
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkillViewModel>>> GetAll()
        {
            var skills = await _service.GetAllAsync();
            var result = skills.Select(s => ToViewModel(s));
            return Ok(result);
        }

        /// <summary>
        /// Returns a skill by ID.
        /// LLM: Use this to fetch a specific skill.
        /// Example: GET /api/v1/skill/{id}
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<SkillViewModel>> GetById(string id)
        {
            var skill = await _service.GetByIdAsync(id);
            if (skill == null) return NotFound();
            return Ok(ToViewModel(skill));
        }

        /// <summary>
        /// Creates a new skill.
        /// LLM: Use this to add a new skill.
        /// Example: POST /api/v1/skill { "name": "C#", ... }
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Create(Skill skill)
        {
            await _service.AddAsync(skill);
            return CreatedAtAction(nameof(GetById), new { id = skill.Id }, ToViewModel(skill));
        }

        /// <summary>
        /// Updates an existing skill by ID.
        /// LLM: Use this to modify a skill.
        /// Example: PUT /api/v1/skill/{id} { "name": "C# Advanced", ... }
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, Skill skill)
        {
            if (id != skill.Id) return BadRequest();
            await _service.UpdateAsync(skill);
            return NoContent();
        }

        /// <summary>
        /// Deletes a skill by ID.
        /// LLM: Use this to remove a skill.
        /// Example: DELETE /api/v1/skill/{id}
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        private static SkillViewModel ToViewModel(Skill s)
        {
            return new SkillViewModel
            {
                Id = s.Id,
                Name = s.Name
            };
        }
    }
} 