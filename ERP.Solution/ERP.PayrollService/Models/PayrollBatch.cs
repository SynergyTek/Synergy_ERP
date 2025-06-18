using System;
using System.Collections.Generic;

namespace ERP.PayrollService.Models
{
    /// <summary>
    /// Represents a batch of payslips generated for a specific payroll calendar period.
    /// </summary>
    public class PayrollBatch
    {
        /// <example>1</example>
        public int Id { get; set; }

        /// <example>January 2025 Payroll</example>
        public string Name { get; set; }

        /// <example>2025-01-31T00:00:00</example>
        public DateTime CreatedAt { get; set; }

        /// <example>[Payslip1, Payslip2]</example>
        public ICollection<Payslip> Payslips { get; set; }

        /// <example>{ "Id": 3, "PeriodStart": "2025-01-01", "PeriodEnd": "2025-01-31" }</example>
        public PayrollCalendar PayrollCalendar { get; set; }

        /// <example>3</example>
        public int PayrollCalendarId { get; set; }
    }
}
