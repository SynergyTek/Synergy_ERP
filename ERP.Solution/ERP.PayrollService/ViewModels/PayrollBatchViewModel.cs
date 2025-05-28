using System;
using System.Collections.Generic;

namespace ERP.PayrollService.ViewModels
{
    public class PayrollBatchViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<PayslipViewModel> Payslips { get; set; }
        public PayrollCalendarViewModel PayrollCalendar { get; set; }
    }
} 