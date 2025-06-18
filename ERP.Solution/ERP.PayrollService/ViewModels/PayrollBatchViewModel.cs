using System;
using System.Collections.Generic;

namespace ERP.PayrollService.ViewModels
{
    /// <summary>
    /// Represents a view model for a payroll batch,
    /// including a list of generated payslips and the associated payroll calendar.
    /// </summary>
    public class PayrollBatchViewModel
    {
        /// <example>1</example>
        public int Id { get; set; }

        /// <example>April 2025 Payroll</example>
        public string Name { get; set; }

        /// <example>2025-04-30T00:00:00</example>
        public DateTime CreatedAt { get; set; }

        public List<PayslipViewModel> Payslips { get; set; }

        public PayrollCalendarViewModel PayrollCalendar { get; set; }
    }
}
