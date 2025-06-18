using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.ViewModels;
using ERP.PayrollService.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
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
        /// <summary>
        /// Get all PayRoll structures 
        /// </summary>
        /// <returns>Get the PayRollstructures Details</returns>
        // Payroll Structure Endpoints
        [HttpGet("structures")]
        public async Task<IEnumerable<PayrollStructureViewModel>> GetStructures() => await _payrollService.GetPayrollStructuresAsync();
        /// <summary>
        /// Get PayRollStructure by Id
        /// </summary>
        /// <param name="id">PayRoll Id</param>
        /// <returns>PayRoll Structure detail</returns>
        
        [HttpGet("structures/{id}")]
        public async Task<ActionResult<PayrollStructureViewModel>> GetStructure(int id)
        {
            var result = await _payrollService.GetPayrollStructureByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Creates a new PayrollStructure
        /// </summary>
        /// <param name="ViewModel">The PayrollStructure data to create</param>
        /// <returns>Created PayrollStructure ID</returns>
        [HttpPost("Createstructures")]
        public async Task<ActionResult<PayrollStructureViewModel>> CreateStructure(PayrollStructureViewModel ViewModel)
        {
            var result = await _payrollService.CreatePayrollStructureAsync(ViewModel);
            return CreatedAtAction(nameof(GetStructure), new { id = result.Id }, result);
        }

        /// <summary>
        /// updates the existing PayrollStructure details
        /// </summary>
        /// <param name="id">Leave ID</param>
        /// <param name="ViewModel">PayrollStructure ViewModel </param>
        /// <returns>update the PayrollStructure details</returns>
        [HttpPut("structures/{id}")]
        public async Task<ActionResult<PayrollStructureViewModel>> UpdateStructure(int id, PayrollStructureViewModel ViewModel)
        {
            if (id != ViewModel.Id) return BadRequest();
            var result = await _payrollService.UpdatePayrollStructureAsync(ViewModel);
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Delete the PayrollStructure Id
        /// </summary>
        /// <param name="id">PayrollStructure Id</param>
        /// <returns>Delete PayrollStructure Id</returns>

        [HttpDelete("structures/{id}")]
        public async Task<IActionResult> DeleteStructure(int id)
        {
            var deleted = await _payrollService.DeletePayrollStructureAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
        /// <summary>
        /// Generate new Payslip
        /// </summary>
        /// <param name="request">Payslip to create</param>
        /// <returns>Generated Payslip</returns>
        // Payslip Endpoints
        [HttpPost("payslips/generate")]
        public async Task<ActionResult<PayslipViewModel>> GeneratePayslip([FromBody] GeneratePayslipRequest request)
        {
            var result = await _payslipService.GeneratePayslipAsync(request.EmployeeId, request.StructureId, request.PeriodStart, request.PeriodEnd);
            if (result == null) return BadRequest();
            return Ok(result);
        }
        /// <summary>
        /// Generate Payslips Batch
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Generated Payslips Batch</returns>
        [HttpPost("payslips/generate-batch")]
        public async Task<ActionResult<IEnumerable<PayslipViewModel>>> GeneratePayslipsBatch([FromBody] GeneratePayslipsBatchRequest request)
        {
            var result = await _payslipService.GeneratePayslipsBatchAsync(request.EmployeeIds, request.StructureId, request.PeriodStart, request.PeriodEnd);
            return Ok(result);
        }
        /// <summary>
        /// Get Payslips for Employee by employeeId
        /// </summary>
        /// <param name="employeeId"> Employee Id</param>
        /// <returns> Get Payslips Details for Employee</returns>
        [HttpGet("payslips/employee/{employeeId}")]
        public async Task<ActionResult<IEnumerable<PayslipViewModel>>> GetPayslipsForEmployee(int employeeId)
        {
            var result = await _payslipService.GetPayslipsForEmployeeAsync(employeeId);
            return Ok(result);
        }
        /// <summary>
        /// Get Payslip by payslipId
        /// </summary>
        /// <param name="payslipId">payslipId</param>
        /// <returns>Get payslip detail by payslipId</returns>
        [HttpGet("payslips/{payslipId}")]
        public async Task<ActionResult<PayslipViewModel>> GetPayslipById(int payslipId)
        {
            var result = await _payslipService.GetPayslipByIdAsync(payslipId);
            if (result == null) return NotFound();
            return Ok(result);
        }
        /// <summary>
        /// DownloadPayslip Pdf by  payslipId
        /// </summary>
        /// <param name="payslipId">payslipId</param>
        /// <returns>Download the PayslipPdf</returns>
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
        /// <summary>
        /// Submit Payslip 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Submitted Payslip</returns>
        // Payslip Workflow Endpoints
        [HttpPost("payslips/{id}/submit")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> SubmitPayslip(int id)
        {
            var result = await _payslipService.UpdatePayslipStatusAsync(id, "Submitted");
            if (!result) return NotFound();
            return Ok();
        }
        /// <summary>
        /// Approves a payslip by updating its status to "Approved".
        /// </summary>
        /// <param name="id">The unique identifier of the payslip to approve</param>
        /// <returns>Returns 200 OK if the update is successful; otherwise, 404 Not Found</returns>
        [HttpPost("payslips/{id}/approve")]
        [Authorize(Roles = "PayrollManager,HRManager")]
        public async Task<IActionResult> ApprovePayslip(int id)
        {
            var result = await _payslipService.UpdatePayslipStatusAsync(id, "Approved");
            if (!result) return NotFound();
            return Ok();
        }

        /// <summary>
        /// Rejects a payslip by updating its status to "Rejected" and recording the reason.
        /// </summary>
        /// <param name="id">The unique identifier of the payslip to reject.</param>
        /// <param name="reason">The reason for rejecting the payslip.</param>
        /// <returns>Returns 200 OK if the update is successful; otherwise, 404 Not Found.</returns>

        [HttpPost("payslips/{id}/reject")]
        [Authorize(Roles = "PayrollManager,HRManager")]
        public async Task<IActionResult> RejectPayslip(int id, [FromBody] string reason)
        {
            var result = await _payslipService.UpdatePayslipStatusAsync(id, "Rejected", reason);
            if (!result) return NotFound();
            return Ok();
        }
        /// <summary>
        /// Marks a payslip as paid by updating its status to "Paid".
        /// </summary>
        /// <param name="id">The unique identifier of the payslip to mark as paid.</param>
        /// <returns>Returns 200 OK if the update is successful; otherwise, 404 Not Found.</returns>

        [HttpPost("payslips/{id}/mark-paid")]
        [Authorize(Roles = "PayrollManager,HRManager")]
        public async Task<IActionResult> MarkPayslipPaid(int id)
        {
            var result = await _payslipService.UpdatePayslipStatusAsync(id, "Paid");
            if (!result) return NotFound();
            return Ok();
        }

        /// <summary>
        /// Retrieves a report of payslips within the specified date range.
        /// </summary>
        /// <param name="periodStart">The start date of the report period.</param>
        /// <param name="periodEnd">The end date of the report period.</param>
        /// <returns>A list of payslip records within the specified period.</returns>

        // Reporting Endpoints
        [HttpGet("report/payslips")]
        public async Task<ActionResult<IEnumerable<PayslipViewModel>>> GetPayslipsReport([FromQuery] string periodStart, [FromQuery] string periodEnd)
        {
            var result = await _reportService.GetPayslipsReportAsync(periodStart, periodEnd);
            return Ok(result);
        }
        /// <summary>
        /// Retrieves a report of Total Payroll within the specified date range.
        /// </summary>
        /// <param name="periodStart">Starting period</param>
        /// <param name="periodEnd">Ending period</param>
        /// <returns> A list of TotalPayroll Report Details</returns>
        // Reporting Endpoints
        [HttpGet("report/total-payroll")]
        public async Task<ActionResult<decimal>> GetTotalPayroll([FromQuery] string periodStart, [FromQuery] string periodEnd)
        {
            var result = await _reportService.GetTotalPayrollAsync(periodStart, periodEnd);
            return Ok(result);
        }
        /// <summary>
        /// Get All PayrollCalendar Details
        /// </summary>
        /// <returns> All PayrollCalendar Details</returns>
        // Payroll Calendar Endpoints
        [HttpGet("calendars")]
        public async Task<IEnumerable<PayrollCalendarViewModel>> GetCalendars() => await _calendarService.GetCalendarsAsync();
        /// <summary>
        /// Get specific PayrollCalendar Detail by Id 
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Get PayrollCalendar detail</returns>
        [HttpGet("calendars/{id}")]
        public async Task<ActionResult<PayrollCalendarViewModel>> GetCalendar(int id)
        {
            var result = await _calendarService.GetCalendarByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
        /// <summary>
        /// Creates a new PayrollCalendar
        /// </summary>
        /// <param name="ViewModel">The PayrollCalendar data to create</param>
        /// <returns>Created PayrollCalendar ID</returns>
        [HttpPost("Createcalendars")]
        public async Task<ActionResult<PayrollCalendarViewModel>> CreateCalendar(PayrollCalendarViewModel ViewModel)
        {
            var result = await _calendarService.CreateCalendarAsync(ViewModel);
            return CreatedAtAction(nameof(GetCalendar), new { id = result.Id }, result);
        }
        /// <summary>
        /// updates the existing PayrollCalendar details
        /// </summary>
        /// <param name="id">PayrollCalendar Id</param>
        /// <param name="ViewModel">PayrollCalendar ViewModel </param>
        /// <returns>update the PayrollCalendar details</returns>
        [HttpPut("Updatecalendars/{id}")]
        public async Task<ActionResult<PayrollCalendarViewModel>> UpdateCalendar(int id, PayrollCalendarViewModel ViewModel)
        {
            if (id != ViewModel.Id) return BadRequest();
            var result = await _calendarService.UpdateCalendarAsync(ViewModel);
            if (result == null) return NotFound();
            return Ok(result);
        }
        /// <summary>
        /// Delete the PayrollCalendar Id
        /// </summary>
        /// <param name="id">PayrollCalendar Id</param>
        /// <returns>Delete PayrollCalendar Id</returns>
        [HttpDelete("Deletecalendars/{id}")]
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