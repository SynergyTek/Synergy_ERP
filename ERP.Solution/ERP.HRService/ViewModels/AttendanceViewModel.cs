using System;

namespace ERP.HRService.ViewModels
{
    public class AttendanceViewModel
    {
        public string Id { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public string State { get; set; } = string.Empty;
    }
} 