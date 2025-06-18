using System;
using System.Collections.Generic;

namespace ERP.PayrollService.ViewModels
{
    /// <summary>
    /// Represents a view model for a payroll calendar,
    /// defining the payroll schedule and associated payroll batches.
    /// </summary>
    public class PayrollCalendarViewModel
    {
        /// <example>1</example>
        public int Id { get; set; }

        /// <example>Monthly Payroll Calendar</example>
        public string Name { get; set; }

        /// <example>Monthly</example>
        public string Frequency { get; set; } // e.g., Monthly, Biweekly

        /// <example>2025-01-01T00:00:00</example>
        public DateTime StartDate { get; set; }

        public List<PayrollBatchViewModel> PayrollBatches { get; set; }
    }
}
