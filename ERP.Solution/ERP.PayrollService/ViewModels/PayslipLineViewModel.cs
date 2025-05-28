namespace ERP.PayrollService.ViewModels
{
    public class PayslipLineViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
    }
} 