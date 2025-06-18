using System;
using System.Collections.Generic;

namespace ERP.PayrollService.Models
{
    /// <summary>
    /// Represents a payslip issued to an employee for a specific payroll period.
    /// Includes gross and net pay, payroll structure, and status.
    /// </summary>
    public class Payslip
    {
        /// <example>1</example>
        public int Id { get; set; }

        /// <example>101</example>
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

        /// <example>3</example>
        public int PayrollStructureId { get; set; }

        public PayrollStructure PayrollStructure { get; set; }

        /// <example>2025-01-01T00:00:00</example>
        public DateTime PeriodStart { get; set; }

        /// <example>2025-01-31T00:00:00</example>
        public DateTime PeriodEnd { get; set; }

        /// <example>60000.00</example>
        public decimal GrossPay { get; set; }

        /// <example>54000.00</example>
        public decimal NetPay { get; set; }

        public ICollection<PayslipLine> PayslipLines { get; set; }

        /// <example>5</example>
        public int? PayrollBatchId { get; set; }

        public PayrollBatch PayrollBatch { get; set; }

        public ICollection<WorkedDayInput> WorkedDayInputs { get; set; }

        /// <example>INR</example>
        public string Currency { get; set; }

        public ICollection<PayslipAdjustment> PayslipAdjustments { get; set; }

        /// <example>Approved</example>
        public string Status { get; set; } // Draft, Submitted, Approved, Rejected, Paid
    }
}
