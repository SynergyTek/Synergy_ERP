using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.ViewModels;
using ERP.PayrollService.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ERP.PayrollService.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace ERP.PayrollService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayrollController : ControllerBase
    {
        private readonly IPayrollService _payrollService;
        private readonly IPayslipService _payslipService;
        private readonly IPayrollReportService _reportService;
        private readonly IPayrollCalendarService _calendarService;

        public PayrollController(
            IPayrollService payrollService,
            IPayslipService payslipService,
            IPayrollReportService reportService,
            IPayrollCalendarService calendarService)
        {
            _payrollService = payrollService;
            _payslipService = payslipService;
            _reportService = reportService;
            _calendarService = calendarService;
        }

        // Payroll Structure Endpoints
        [HttpGet("structures")]
        public async Task<IEnumerable<PayrollStructureViewModel>> GetStructures() => await _payrollService.GetPayrollStructuresAsync();

        [HttpGet("structures/{id}")]
        public async Task<ActionResult<PayrollStructureViewModel>> GetStructure(int id)
        {
            var result = await _payrollService.GetPayrollStructureByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost("structures")]
        public async Task<ActionResult<PayrollStructureViewModel>> CreateStructure(PayrollStructureViewModel ViewModel)
        {
            var result = await _payrollService.CreatePayrollStructureAsync(ViewModel);
            return CreatedAtAction(nameof(GetStructure), new { id = result.Id }, result);
        }

        [HttpPut("structures/{id}")]
        public async Task<ActionResult<PayrollStructureViewModel>> UpdateStructure(int id, PayrollStructureViewModel ViewModel)
        {
            if (id != ViewModel.Id) return BadRequest();
            var result = await _payrollService.UpdatePayrollStructureAsync(ViewModel);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("structures/{id}")]
        public async Task<IActionResult> DeleteStructure(int id)
        {
            var deleted = await _payrollService.DeletePayrollStructureAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        // Payslip Endpoints
        [HttpPost("payslips/generate")]
        public async Task<ActionResult<PayslipViewModel>> GeneratePayslip([FromBody] GeneratePayslipRequest request)
        {
            var result = await _payslipService.GeneratePayslipAsync(request.EmployeeId, request.StructureId, request.PeriodStart, request.PeriodEnd);
            if (result == null) return BadRequest();
            return Ok(result);
        }

        [HttpPost("payslips/generate-batch")]
        public async Task<ActionResult<IEnumerable<PayslipViewModel>>> GeneratePayslipsBatch([FromBody] GeneratePayslipsBatchRequest request)
        {
            var result = await _payslipService.GeneratePayslipsBatchAsync(request.EmployeeIds, request.StructureId, request.PeriodStart, request.PeriodEnd);
            return Ok(result);
        }

        [HttpGet("payslips/employee/{employeeId}")]
        public async Task<ActionResult<IEnumerable<PayslipViewModel>>> GetPayslipsForEmployee(int employeeId)
        {
            var result = await _payslipService.GetPayslipsForEmployeeAsync(employeeId);
            return Ok(result);
        }

        [HttpGet("payslips/{payslipId}")]
        public async Task<ActionResult<PayslipViewModel>> GetPayslipById(int payslipId)
        {
            var result = await _payslipService.GetPayslipByIdAsync(payslipId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("payslips/{payslipId}/pdf")]
        public async Task<IActionResult> DownloadPayslipPdf(int payslipId)
        {
            // Fetch payslip data (for demo, use payslip service)
            var payslip = await _payslipService.GetPayslipByIdAsync(payslipId);
            if (payslip == null) return NotFound();

            var pdfBytes = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.Content()
                        .Column(col =>
                        {
                            col.Item().Text($"Payslip #{payslip.Id}").FontSize(20).Bold();
                            col.Item().Text($"Employee: {payslip.Employee.FirstName} {payslip.Employee.LastName}");
                            col.Item().Text($"Period: {payslip.PeriodStart:yyyy-MM-dd} to {payslip.PeriodEnd:yyyy-MM-dd}");
                            col.Item().Text($"Gross Pay: {payslip.GrossPay:C}");
                            col.Item().Text($"Net Pay: {payslip.NetPay:C}");
                            col.Item().Text("");
                            col.Item().Text("Details:").Bold();
                            foreach (var line in payslip.PayslipLines ?? new List<ERP.PayrollService.ViewModels.PayslipLineViewModel>())
                            {
                                col.Item().Text($"{line.Type}: {line.Name} - {line.Amount:C}");
                            }
                        });
                });
            }).GeneratePdf();

            return File(pdfBytes, "application/pdf", $"payslip_{payslipId}.pdf");
        }

        // Payslip Workflow Endpoints
        [HttpPost("payslips/{id}/submit")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> SubmitPayslip(int id)
        {
            var result = await _payslipService.UpdatePayslipStatusAsync(id, "Submitted");
            if (!result) return NotFound();
            return Ok();
        }

        [HttpPost("payslips/{id}/approve")]
        [Authorize(Roles = "PayrollManager,HRManager")]
        public async Task<IActionResult> ApprovePayslip(int id)
        {
            var result = await _payslipService.UpdatePayslipStatusAsync(id, "Approved");
            if (!result) return NotFound();
            return Ok();
        }

        [HttpPost("payslips/{id}/reject")]
        [Authorize(Roles = "PayrollManager,HRManager")]
        public async Task<IActionResult> RejectPayslip(int id, [FromBody] string reason)
        {
            var result = await _payslipService.UpdatePayslipStatusAsync(id, "Rejected", reason);
            if (!result) return NotFound();
            return Ok();
        }

        [HttpPost("payslips/{id}/mark-paid")]
        [Authorize(Roles = "PayrollManager,HRManager")]
        public async Task<IActionResult> MarkPayslipPaid(int id)
        {
            var result = await _payslipService.UpdatePayslipStatusAsync(id, "Paid");
            if (!result) return NotFound();
            return Ok();
        }

        // Reporting Endpoints
        [HttpGet("report/payslips")]
        public async Task<ActionResult<IEnumerable<PayslipViewModel>>> GetPayslipsReport([FromQuery] string periodStart, [FromQuery] string periodEnd)
        {
            var result = await _reportService.GetPayslipsReportAsync(periodStart, periodEnd);
            return Ok(result);
        }

        [HttpGet("report/total-payroll")]
        public async Task<ActionResult<decimal>> GetTotalPayroll([FromQuery] string periodStart, [FromQuery] string periodEnd)
        {
            var result = await _reportService.GetTotalPayrollAsync(periodStart, periodEnd);
            return Ok(result);
        }

        // Payroll Calendar Endpoints
        [HttpGet("calendars")]
        public async Task<IEnumerable<PayrollCalendarViewModel>> GetCalendars() => await _calendarService.GetCalendarsAsync();

        [HttpGet("calendars/{id}")]
        public async Task<ActionResult<PayrollCalendarViewModel>> GetCalendar(int id)
        {
            var result = await _calendarService.GetCalendarByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost("calendars")]
        public async Task<ActionResult<PayrollCalendarViewModel>> CreateCalendar(PayrollCalendarViewModel ViewModel)
        {
            var result = await _calendarService.CreateCalendarAsync(ViewModel);
            return CreatedAtAction(nameof(GetCalendar), new { id = result.Id }, result);
        }

        [HttpPut("calendars/{id}")]
        public async Task<ActionResult<PayrollCalendarViewModel>> UpdateCalendar(int id, PayrollCalendarViewModel ViewModel)
        {
            if (id != ViewModel.Id) return BadRequest();
            var result = await _calendarService.UpdateCalendarAsync(ViewModel);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("calendars/{id}")]
        public async Task<IActionResult> DeleteCalendar(int id)
        {
            var deleted = await _calendarService.DeleteCalendarAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }

    // Request models for batch endpoints
    public class GeneratePayslipRequest
    {
        public int EmployeeId { get; set; }
        public int StructureId { get; set; }
        public string PeriodStart { get; set; }
        public string PeriodEnd { get; set; }
    }
    public class GeneratePayslipsBatchRequest
    {
        public List<int> EmployeeIds { get; set; }
        public int StructureId { get; set; }
        public string PeriodStart { get; set; }
        public string PeriodEnd { get; set; }
    }
} 