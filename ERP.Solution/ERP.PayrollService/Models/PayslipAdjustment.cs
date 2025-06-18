using System;

namespace ERP.PayrollService.Models
{
    /// <summary>
    /// Represents an adjustment made to a payslip, such as a bonus or correction.
    /// </summary>
    public class PayslipAdjustment
    {
        /// <example>1</example>
        public int Id { get; set; }

        /// <example>101</example>
        public int PayslipId { get; set; }

        public Payslip Payslip { get; set; }

        /// <example>Year-end bonus adjustment</example>
        public string Description { get; set; }

        /// <example>5000.00</example>
        public decimal Amount { get; set; }
    }
}
