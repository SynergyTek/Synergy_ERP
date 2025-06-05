using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.ViewModels;
using ERP.PayrollService.Interfaces;
using System.Linq;
using System.Text;

namespace ERP.PayrollService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayrollReportController : ControllerBase
    {
        private readonly IPayrollReportService _reportService;
        public PayrollReportController(IPayrollReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("summary-by-department")]
        public async Task<ActionResult<IEnumerable<object>>> GetSummaryByDepartment([FromQuery] string periodStart, [FromQuery] string periodEnd)
        {
            var payslips = await _reportService.GetPayslipsReportAsync(periodStart, periodEnd);
            var summary = payslips
                .GroupBy(p => p.Employee.Department)
                .Select(g => new {
                    Department = g.Key,
                    TotalGross = g.Sum(x => x.GrossPay),
                    TotalNet = g.Sum(x => x.NetPay),
                    EmployeeCount = g.Count()
                });
            return Ok(summary);
        }

        [HttpGet("employee-history/{employeeId}")]
        public async Task<ActionResult<IEnumerable<PayslipViewModel>>> GetEmployeeHistory(int employeeId)
        {
            var payslips = await _reportService.GetPayslipsForEmployeeAsync(employeeId);
            return Ok(payslips);
        }

        [HttpGet("export-csv")]
        public async Task<IActionResult> ExportPayrollCsv([FromQuery] string periodStart, [FromQuery] string periodEnd)
        {
            var payslips = await _reportService.GetPayslipsReportAsync(periodStart, periodEnd);
            var sb = new StringBuilder();
            sb.AppendLine("Employee,Department,PeriodStart,PeriodEnd,GrossPay,NetPay");
            foreach (var p in payslips)
            {
                sb.AppendLine($"{p.Employee?.FirstName} {p.Employee?.LastName},{p.Employee?.Department},{p.PeriodStart:yyyy-MM-dd},{p.PeriodEnd:yyyy-MM-dd},{p.GrossPay},{p.NetPay}");
            }
            var bytes = Encoding.UTF8.GetBytes(sb.ToString());
            return File(bytes, "text/csv", "payroll_report.csv");
        }
    }
} 