using System;
using System.Collections.Generic;

namespace ERP.PayrollService.Models
{
    public class Payslip
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int PayrollStructureId { get; set; }
        public PayrollStructure PayrollStructure { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public decimal GrossPay { get; set; }
        public decimal NetPay { get; set; }
        public ICollection<PayslipLine> PayslipLines { get; set; }
        public int? PayrollBatchId { get; set; }
        public PayrollBatch PayrollBatch { get; set; }
    }
} 