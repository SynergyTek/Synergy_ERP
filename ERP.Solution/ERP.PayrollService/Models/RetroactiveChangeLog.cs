using System;

namespace ERP.PayrollService.Models
{
    /// <summary>
    /// Represents a record of changes applied retroactively to payroll-related data such as contracts, rules, or allowances.
    /// </summary>
    public class RetroactiveChangeLog
    {
        /// <example>1</example>
        public int Id { get; set; }

        /// <example>102</example>
        public int? EmployeeId { get; set; } // null if global

        /// <example>Allowance</example>
        public string ChangeType { get; set; } // Contract, Rule, Allowance, Deduction, etc.

        /// <example>2025-01-01T00:00:00</example>
        public DateTime AffectedPeriodStart { get; set; }

        /// <example>2025-01-31T00:00:00</example>
        public DateTime AffectedPeriodEnd { get; set; }

        /// <example>2025-06-15T12:00:00</example>
        public DateTime ChangeDate { get; set; }

        /// <example>Updated HRA allowance for January period</example>
        public string Description { get; set; }
    }
}
