using System;

namespace ERP.PayrollService.Models
{
    /// <summary>
    /// Represents a loan taken by an employee, including amount, repayment status, and remaining balance.
    /// </summary>
    public class Loan
    {
        /// <example>1</example>
        public int Id { get; set; }

        /// <example>101</example>
        public int EmployeeId { get; set; }

        /// <example>{
        ///   "Name": "John Doe",
        ///   "Email": "john.doe@example.com",
        ///   "Designation": "Software Engineer",
        ///   "BaseSalary": 50000,
        ///   "Position": "Software Engineer"
        /// }</example>
        public Employee Employee { get; set; }

        /// <example>50000.00</example>
        public decimal Amount { get; set; }

        /// <example>5000.00</example>
        public decimal Installment { get; set; }

        /// <example>15000.00</example>
        public decimal Remaining { get; set; }

        /// <example>Active</example>
        public string Status { get; set; } // e.g., Active, Closed

        /// <example>Personal loan for medical emergency</example>
        public string Description { get; set; }
    }
}
