namespace ERP.PayrollService.ViewModels
{   /// <summary>
    /// Represents an allowance component in a payroll structure.
    /// </summary>
    public class AllowanceViewModel
    {   /// <example>5</example>
        public int Id { get; set; }
        /// <example>John</example>
        public string Name { get; set; }
        /// <example>5000.00</example>
        public decimal Amount { get; set; }
    }
} 