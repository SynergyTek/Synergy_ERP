using System;

namespace ERP.PayrollService.Models
{   /// <summary>
    /// Represents a leave request made by an employee, including type, status, and duration.
    /// </summary>
    public class Leave
    {
        public int Id { get; set; }
        /// <example>202</example>
        public int EmployeeId { get; set; }
        /// <example>{
        ///   "Name": "John Doe",
        ///   "Email": "john.doe@example.com",
        ///   "Designation": "Software Engineer",
        ///   "BaseSalary": 50000,
        ///   "Position": "Software Engineer"
        /// }</example>
        public Employee Employee { get; set; }
        /// <example>2025-01-01T00:00:00</example>
        public DateTime StartDate { get; set; }
        /// <example>2026-01-01T00:00:00</example>
        public DateTime EndDate { get; set; }
        /// <example>paid</example>
        public string Type { get; set; } // e.g., Paid, Unpaid, Sick
        /// <example>Approved</example>
        public string Status { get; set; } // e.g., Approved, Pending, Rejected
        /// <example>sick leave</example>
        public string Description { get; set; }
    }
} 