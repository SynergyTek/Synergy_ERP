using System;

namespace ERP.HRService.Models
{
    public class Leave
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string LeaveType { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
    }
} 