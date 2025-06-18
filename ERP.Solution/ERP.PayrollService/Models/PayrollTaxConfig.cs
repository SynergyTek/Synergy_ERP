namespace ERP.PayrollService.Models
{
    /// <summary>
    /// Represents the configuration for payroll tax, including rate, threshold, and active status.
    /// </summary>
    public class PayrollTaxConfig
    {
        /// <example>1</example>
        public int Id { get; set; }

        /// <example>Professional Tax</example>
        public string Name { get; set; }

        /// <example>0.05</example>
        public decimal Rate { get; set; }

        /// <example>25000</example>
        public decimal Threshold { get; set; }

        /// <example>true</example>
        public bool IsActive { get; set; }
    }
}
