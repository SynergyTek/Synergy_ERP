using System;
using System.Collections.Generic;

namespace ERP.PayrollService.ViewModels
{
    public class PayrollCalendarViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Frequency { get; set; }
        public DateTime StartDate { get; set; }
        public List<PayrollBatchViewModel> PayrollBatches { get; set; }
    }
} 