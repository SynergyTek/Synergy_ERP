using System;

namespace ERP.PayrollService.Models
{
    public class Leave
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Type { get; set; } // e.g., Paid, Unpaid, Sick
        public string Status { get; set; } // e.g., Approved, Pending, Rejected
        public string Description { get; set; }
    }
} 