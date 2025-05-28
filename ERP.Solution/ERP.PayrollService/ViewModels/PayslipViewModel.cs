using System;
using System.Collections.Generic;

namespace ERP.PayrollService.ViewModels
{
    public class PayslipViewModel
    {
        public int Id { get; set; }
        public EmployeeViewModel Employee { get; set; }
        public PayrollStructureViewModel PayrollStructure { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public decimal GrossPay { get; set; }
        public decimal NetPay { get; set; }
        public List<PayslipLineViewModel> PayslipLines { get; set; }
        public PayrollBatchViewModel PayrollBatch { get; set; }
    }
} 