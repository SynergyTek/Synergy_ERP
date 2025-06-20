using System;

namespace ERP.HRService.Models
{
    public class Contract
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ContractType { get; set; } = string.Empty;
        public decimal Wage { get; set; }
        public string State { get; set; } = string.Empty;
    }
} 