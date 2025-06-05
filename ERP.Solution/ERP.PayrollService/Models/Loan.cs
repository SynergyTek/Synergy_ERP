using System;

namespace ERP.PayrollService.Models
{
    public class Loan
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public decimal Amount { get; set; }
        public decimal Installment { get; set; }
        public decimal Remaining { get; set; }
        public string Status { get; set; } // e.g., Active, Closed
        public string Description { get; set; }
    }
} 