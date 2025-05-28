namespace ERP.PayrollService.Models
{
    public class Allowance
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public int PayrollStructureId { get; set; }
        public PayrollStructure PayrollStructure { get; set; }
    }
} 