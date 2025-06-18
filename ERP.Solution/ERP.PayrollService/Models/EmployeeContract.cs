using System;

namespace ERP.PayrollService.Models
{
    public class EmployeeContract
    {
        public int Id { get; set; }

        /// <example>101</example>
        public int EmployeeId { get; set; }

        /// <example>2025-01-01T00:00:00</example>
        public DateTime StartDate { get; set; }

        /// <example>2026-12-31T00:00:00</example>
        public DateTime? EndDate { get; set; }

        /// <example>USD</example>
        public string Currency { get; set; }

        /// <example>United States</example>
        public string Country { get; set; }

        /// <example>75000.00</example>
        public decimal Wage { get; set; }

        /// <example>Senior Developer</example>
        public string JobTitle { get; set; }

        /// <example>Full-Time</example>
        public string ContractType { get; set; }

        /// <example>true</example>
        public bool IsActive { get; set; }

        public Employee Employee { get; set; }
    }
}
