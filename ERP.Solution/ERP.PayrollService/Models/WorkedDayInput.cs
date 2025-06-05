using System;

namespace ERP.PayrollService.Models
{
    public class WorkedDayInput
    {
        public int Id { get; set; }
        public int PayslipId { get; set; }
        public Payslip Payslip { get; set; }
        public string Type { get; set; } // Worked, Overtime, Absence, etc.
        public decimal NumberOfDays { get; set; }
        public decimal NumberOfHours { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
} 