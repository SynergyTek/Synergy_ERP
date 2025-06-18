namespace ERP.PayrollService.Models
{
    /// <summary>
    /// Represents a detailed line item in a payslip, such as an allowance, deduction, or rule-based calculation.
    /// </summary>
    public class PayslipLine
    {
        /// <example>1</example>
        public int Id { get; set; }

        /// <example>101</example>
        public int PayslipId { get; set; }

        public Payslip Payslip { get; set; }

        /// <example>Allowance</example>
        public string Type { get; set; } // Allowance, Deduction, Rule

        /// <example>HRA</example>
        public string Name { get; set; }

        /// <example>2000.00</example>
        public decimal Amount { get; set; }
    }
}
