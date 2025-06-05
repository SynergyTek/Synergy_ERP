using System;

namespace ERP.PayrollService.Models
{
    public class PayslipAdjustment
    {
        public int Id { get; set; }
        public int PayslipId { get; set; }
        public Payslip Payslip { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
} 