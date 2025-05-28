using System;
using System.Collections.Generic;

namespace ERP.PayrollService.Models
{
    public class PayrollBatch
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Payslip> Payslips { get; set; }
        public PayrollCalendar PayrollCalendar { get; set; }
        public int PayrollCalendarId { get; set; }
    }
} 