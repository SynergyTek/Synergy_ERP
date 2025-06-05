using System;

namespace ERP.PayrollService.Models
{
    public class PayslipStatusChangeLog
    {
        public int Id { get; set; }
        public int PayslipId { get; set; }
        public string OldStatus { get; set; }
        public string NewStatus { get; set; }
        public DateTime ChangeDate { get; set; }
        public string ChangedBy { get; set; }
        public string Reason { get; set; }
    }
} 