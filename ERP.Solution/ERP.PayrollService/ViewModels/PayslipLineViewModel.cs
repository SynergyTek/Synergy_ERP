namespace ERP.PayrollService.ViewModels
{
    /// <summary>
    /// Represents a line item in a payslip, including details like type, name, and amount.
    /// </summary>
    public class PayslipLineViewModel
    {
        /// <example>1</example>
        public int Id { get; set; }

        /// <example>Basic Salary </example>
        public string Description { get; set; }

        /// <example>Allowance</example>
        public string Type { get; set; } // e.g., Allowance, Deduction, Rule

        /// <example>Basic Salary</example>
        public string Name { get; set; }

        /// <example>25000.00</example>
        public decimal Amount { get; set; }
    }
}
