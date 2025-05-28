using System.Collections.Generic;

namespace ERP.PayrollService.Models
{
    public class PayrollStructure
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<PayrollRule> Rules { get; set; }
        public ICollection<Allowance> Allowances { get; set; }
        public ICollection<Deduction> Deductions { get; set; }
    }
} 