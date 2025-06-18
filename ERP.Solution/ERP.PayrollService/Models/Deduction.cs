namespace ERP.PayrollService.Models
{    /// <summary>
     /// Represents an deduction  component in a payroll structure.
     /// </summary>
    public class Deduction
    {
        public int Id { get; set; }
        /// <example>John</example>
        public string Name { get; set; }
        /// <example>500.00</example>
        public decimal Amount { get; set; }
        /// <example>1</example>
        public int PayrollStructureId { get; set; }
        public PayrollStructure PayrollStructure { get; set; }
    }
} 