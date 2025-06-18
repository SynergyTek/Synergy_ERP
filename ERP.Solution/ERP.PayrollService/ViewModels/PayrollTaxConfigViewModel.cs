namespace ERP.PayrollService.ViewModels
{
    /// <summary>
    /// Represents the view model for payroll tax configuration,
    /// including tax rate, threshold, and active status.
    /// </summary>
    public class PayrollTaxConfigViewModel
    {
        /// <example>1</example>
        public int Id { get; set; }

        /// <example>Income Tax</example>
        public string Name { get; set; }

        /// <example>15.5</example>
        public decimal Rate { get; set; }

        /// <example>25000</example>
        public decimal Threshold { get; set; }

        /// <example>true</example>
        public bool IsActive { get; set; }
    }
}
