using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.PayrollService.Interfaces
{
    public interface IPayrollAIBotService
    {
        Task<string> AskPayrollBotAsync(string userMessage);
        Task<string> GeneratePayslipAsync(int employeeId, int structureId, string periodStart, string periodEnd);
        Task<string> GeneratePayslipsBatchAsync(List<int> employeeIds, int structureId, string periodStart, string periodEnd);
        Task<string> GetPayslipsForEmployeeAsync(int employeeId);
        Task<string> GetPayslipByIdAsync(int payslipId);
        Task<string> GetPayslipsReportAsync(string periodStart, string periodEnd);
        Task<string> GetTotalPayrollAsync(string periodStart, string periodEnd);
        Task<string> GetPayrollStructuresAsync();
        Task<string> GetPayrollStructureByIdAsync(int id);
    }
} 