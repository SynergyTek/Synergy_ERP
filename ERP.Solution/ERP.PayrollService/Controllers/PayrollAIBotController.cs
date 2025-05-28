using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.Interfaces;

namespace ERP.PayrollService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayrollAIBotController : ControllerBase
    {
        private readonly IPayrollAIBotService _botService;
        public PayrollAIBotController(IPayrollAIBotService botService)
        {
            _botService = botService;
        }

        [HttpPost("chat")]
        public async Task<IActionResult> Chat([FromBody] ChatRequest request)
        {
            // Simple keyword-based intent recognition
            var msg = request.Message.ToLower();
            string response;
            if (msg.Contains("generate payslip batch"))
            {
                response = await _botService.GeneratePayslipsBatchAsync(request.EmployeeIds, request.StructureId ?? 0, request.PeriodStart, request.PeriodEnd);
            }
            else if (msg.Contains("generate payslip"))
            {
                response = await _botService.GeneratePayslipAsync(request.EmployeeId ?? 0, request.StructureId ?? 0, request.PeriodStart, request.PeriodEnd);
            }
            else if (msg.Contains("payslips for employee"))
            {
                response = await _botService.GetPayslipsForEmployeeAsync(request.EmployeeId ?? 0);
            }
            else if (msg.Contains("payslip by id"))
            {
                response = await _botService.GetPayslipByIdAsync(request.PayslipId ?? 0);
            }
            else if (msg.Contains("payslips report"))
            {
                response = await _botService.GetPayslipsReportAsync(request.PeriodStart, request.PeriodEnd);
            }
            else if (msg.Contains("total payroll"))
            {
                response = await _botService.GetTotalPayrollAsync(request.PeriodStart, request.PeriodEnd);
            }
            else if (msg.Contains("payroll structures"))
            {
                response = await _botService.GetPayrollStructuresAsync();
            }
            else if (msg.Contains("payroll structure by id"))
            {
                response = await _botService.GetPayrollStructureByIdAsync(request.StructureId ?? 0);
            }
            else
            {
                response = await _botService.AskPayrollBotAsync(request.Message);
            }
            return Ok(response);
        }
    }

    public class ChatRequest
    {
        public string Message { get; set; }
        public int? EmployeeId { get; set; }
        public List<int> EmployeeIds { get; set; }
        public int? StructureId { get; set; }
        public int? PayslipId { get; set; }
        public string PeriodStart { get; set; }
        public string PeriodEnd { get; set; }
    }
} 