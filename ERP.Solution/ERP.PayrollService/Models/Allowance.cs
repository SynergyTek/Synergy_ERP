namespace ERP.PayrollService.Models
{     /// <summary>
      /// Represents an allowance component in a payroll structure.
      /// </summary>
    public class Allowance
    {
        public int Id { get; set; }
        /// <example>John</example>
        public string Name { get; set; }
        /// <example>5000.00</example>
        public decimal Amount { get; set; }
        /// <example>2</example>
        public int PayrollStructureId { get; set; }
        public PayrollStructure PayrollStructure { get; set; }
    }
} 