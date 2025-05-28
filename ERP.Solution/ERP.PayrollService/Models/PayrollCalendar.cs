using System;
using System.Collections.Generic;

namespace ERP.PayrollService.Models
{
    public class PayrollCalendar
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Frequency { get; set; } // e.g., Monthly, Biweekly
        public DateTime StartDate { get; set; }
        public ICollection<PayrollBatch> PayrollBatches { get; set; }
    }
} 