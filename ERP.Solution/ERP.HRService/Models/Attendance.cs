using System;

namespace ERP.HRService.Models
{
    public class Attendance
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;
        public DateTime CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public string State { get; set; } = string.Empty;
    }
} 