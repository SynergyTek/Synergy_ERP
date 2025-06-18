namespace ERP.PayrollService.ViewModels
{    /// <summary>
     /// Represents an deduction  component in a payroll structure.
     /// </summary>
    public class DeductionViewModel
    {    /// <example>5</example>
        public int Id { get; set; }
        /// <example>John</example>
        public string Name { get; set; }
        /// <example>300.00</example>
        public decimal Amount { get; set; }
    }
} 