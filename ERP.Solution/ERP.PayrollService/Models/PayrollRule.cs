using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.PayrollService.Models
{
    /// <summary>
    /// Represents a payroll rule used to calculate allowances or deductions in the payroll process.
    /// </summary>
    public class PayrollRule
    {
        /// <example>1</example>
        public int Id { get; set; }

        /// <example>HRA Allowance</example>
        public string Name { get; set; }

        /// <example>Allowance</example>
        public string RuleType { get; set; } // e.g., Allowance, Deduction

        /// <example>1000.00</example>
        public decimal Value { get; set; }

        /// <example>2</example>
        public int PayrollStructureId { get; set; }

        [ForeignKey("PayrollStructureId")]
        /// <example>{ "Id": 2, "Name": "Default Structure" }</example>
        public PayrollStructure PayrollStructure { get; set; }

        /// <example>BaseSalary * 0.10 + 500</example>
        public string Formula { get; set; } // e.g., "BaseSalary * 0.10 + 100"

        /// <example>1</example>
        public int Sequence { get; internal set; }

        /// <example>India</example>
        public string Country { get; set; }
    }
}
