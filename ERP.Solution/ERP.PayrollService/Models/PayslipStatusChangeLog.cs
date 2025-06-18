using System;

namespace ERP.PayrollService.Models
{
    /// <summary>
    /// Represents the log of status changes for a payslip, including who made the change and why.
    /// </summary>
    public class PayslipStatusChangeLog
    {
        /// <example>1</example>
        public int Id { get; set; }

        /// <example>101</example>
        public int PayslipId { get; set; }

        /// <example>Draft</example>
        public string OldStatus { get; set; }

        /// <example>Approved</example>
        public string NewStatus { get; set; }

        /// <example>2025-06-18T10:30:00</example>
        public DateTime ChangeDate { get; set; }

        /// <example>admin@example.com</example>
        public string ChangedBy { get; set; }

        /// <example>Reviewed and approved by HR</example>
        public string Reason { get; set; }
    }
}
