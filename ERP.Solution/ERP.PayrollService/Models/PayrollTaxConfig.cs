namespace ERP.PayrollService.Models
{
    public class PayrollTaxConfig
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Rate { get; set; }
        public decimal Threshold { get; set; }
        public bool IsActive { get; set; }
    }
} 