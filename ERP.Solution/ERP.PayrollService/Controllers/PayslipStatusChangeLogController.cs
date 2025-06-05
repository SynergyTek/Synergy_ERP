using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.Models;
using ERP.PayrollService.Interfaces;

namespace ERP.PayrollService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayslipStatusChangeLogController : ControllerBase
    {
        private readonly IPayslipStatusChangeLogRepository _repo;
        public PayslipStatusChangeLogController(IPayslipStatusChangeLogRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<IEnumerable<PayslipStatusChangeLog>> GetAll() => await _repo.GetAllAsync();
        [HttpGet("bypayslip/{payslipId}")]
        public async Task<IEnumerable<PayslipStatusChangeLog>> GetByPayslipId(int payslipId) => await _repo.GetByPayslipIdAsync(payslipId);
    }
} 