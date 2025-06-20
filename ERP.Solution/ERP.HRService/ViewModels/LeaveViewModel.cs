using System;

namespace ERP.HRService.ViewModels
{
    public class LeaveViewModel
    {
        public string Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string LeaveType { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
    }
} 