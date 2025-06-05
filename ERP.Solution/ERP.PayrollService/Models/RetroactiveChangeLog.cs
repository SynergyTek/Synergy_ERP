using System;

namespace ERP.PayrollService.Models
{
    public class RetroactiveChangeLog
    {
        public int Id { get; set; }
        public int? EmployeeId { get; set; } // null if global
        public string ChangeType { get; set; } // Contract, Rule, Allowance, Deduction, etc.
        public DateTime AffectedPeriodStart { get; set; }
        public DateTime AffectedPeriodEnd { get; set; }
        public DateTime ChangeDate { get; set; }
        public string Description { get; set; }
    }
} 