using System;

namespace ERP.PayrollService.Models
{
    /// <summary>
    /// Represents the input for worked days, including types like regular work, overtime, or absence,
    /// used in payroll calculations for a specific payslip.
    /// </summary>
    public class WorkedDayInput
    {
        /// <example>1</example>
        public int Id { get; set; }

        /// <example>101</example>
        public int PayslipId { get; set; }

        /// <example>Worked</example>
        public Payslip Payslip { get; set; }

        /// <example>Overtime</example>
        public string Type { get; set; } // Worked, Overtime, Absence, etc.

        /// <example>22</example>
        public decimal NumberOfDays { get; set; }

        /// <example>176</example>
        public decimal NumberOfHours { get; set; }

        /// <example>3500.75</example>
        public decimal Amount { get; set; }

        /// <example>Extra hours worked on weekends</example>
        public string Description { get; set; }
    }
}
