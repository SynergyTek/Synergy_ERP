using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.PayrollService.Models
{
    public class PayrollRule
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RuleType { get; set; } // e.g., Allowance, Deduction
        public decimal Value { get; set; }
        public int PayrollStructureId { get; set; }
        [ForeignKey("PayrollStructureId")]
        public PayrollStructure PayrollStructure { get; set; }
        public string Formula { get; set; } // e.g., "BaseSalary * 0.10 + 100"
        public int Sequence { get; internal set; }
        public string Country { get; set; }
    }
} 