using System.Collections.Generic;

namespace ERP.PayrollService.Models
{
    /// <summary>
    /// Represents a payroll structure that defines the set of rules, allowances, and deductions applicable for payroll processing in a specific country.
    /// </summary>
    public class PayrollStructure
    {
        /// <example>1</example>
        public int Id { get; set; }

        /// <example>Standard Payroll Structure</example>
        public string Name { get; set; }

        /// <example>Structure applicable to full-time employees</example>
        public string Description { get; set; }

        /// <example>India</example>
        public string Country { get; set; }

        public ICollection<PayrollRule> Rules { get; set; }

        public ICollection<Allowance> Allowances { get; set; }

        public ICollection<Deduction> Deductions { get; set; }
    }
}
