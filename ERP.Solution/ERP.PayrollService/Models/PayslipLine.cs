namespace ERP.PayrollService.Models
{
    public class PayslipLine
    {
        public int Id { get; set; }
        public int PayslipId { get; set; }
        public Payslip Payslip { get; set; }
        public string Type { get; set; } // Allowance, Deduction, Rule
        public string Name { get; set; }
        public decimal Amount { get; set; }
    }
} 