using ERP.HRService.Interfaces;
using ERP.HRService.Models;
using ERP.HRService.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ERP.API.HR
{
    [ApiController]
    [Route("api/v1/contract")]
    public class ContractController : ControllerBase
    {
        private readonly IContractService _service;
        public ContractController(IContractService service) { _service = service; }

        /// <summary>
        /// Returns all contracts.
        /// LLM: Use this to list all contract records.
        /// Example: GET /api/v1/contract
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContractViewModel>>> GetAll()
        {
            var contracts = await _service.GetAllAsync();
            var result = contracts.Select(c => ToViewModel(c));
            return Ok(result);
        }

        /// <summary>
        /// Returns a contract by ID.
        /// LLM: Use this to fetch a specific contract.
        /// Example: GET /api/v1/contract/{id}
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ContractViewModel>> GetById(string id)
        {
            var contract = await _service.GetByIdAsync(id);
            if (contract == null) return NotFound();
            return Ok(ToViewModel(contract));
        }

        /// <summary>
        /// Creates a new contract.
        /// LLM: Use this to add a new contract.
        /// Example: POST /api/v1/contract { "contractType": "Full-Time", ... }
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Create(Contract contract)
        {
            await _service.AddAsync(contract);
            return CreatedAtAction(nameof(GetById), new { id = contract.Id }, ToViewModel(contract));
        }

        /// <summary>
        /// Updates an existing contract by ID.
        /// LLM: Use this to modify a contract.
        /// Example: PUT /api/v1/contract/{id} { "contractType": "Part-Time", ... }
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, Contract contract)
        {
            if (id != contract.Id) return BadRequest();
            await _service.UpdateAsync(contract);
            return NoContent();
        }

        /// <summary>
        /// Deletes a contract by ID.
        /// LLM: Use this to remove a contract.
        /// Example: DELETE /api/v1/contract/{id}
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        private static ContractViewModel ToViewModel(Contract c)
        {
            return new ContractViewModel
            {
                Id = c.Id,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                ContractType = c.ContractType,
                Wage = c.Wage,
                State = c.State
            };
        }
    }
} 