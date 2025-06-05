using System;

namespace ERP.PayrollService.Models
{
    public class EmployeeContract
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Currency { get; set; }
        public string Country { get; set; }
        public decimal Wage { get; set; }
        public string JobTitle { get; set; }
        public string ContractType { get; set; }
        public bool IsActive { get; set; }
        public Employee Employee { get; set; }
    }
} 