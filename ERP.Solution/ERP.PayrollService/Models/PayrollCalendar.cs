using System;
using System.Collections.Generic;

namespace ERP.PayrollService.Models
{
    /// <summary>
    /// Represents a payroll calendar defining the schedule for payroll processing, such as frequency and start date.
    /// </summary>
    public class PayrollCalendar
    {
        /// <example>1</example>
        public int Id { get; set; }

        /// <example>2025 Monthly Calendar</example>
        public string Name { get; set; }

        /// <example>Monthly</example>
        public string Frequency { get; set; } // e.g., Monthly, Biweekly

        /// <example>2025-01-01T00:00:00</example>
        public DateTime StartDate { get; set; }

        /// <example>[PayrollBatch1, PayrollBatch2]</example>
        public ICollection<PayrollBatch> PayrollBatches { get; set; }
    }
}
