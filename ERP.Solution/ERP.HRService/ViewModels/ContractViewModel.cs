using System;

namespace ERP.HRService.ViewModels
{
    public class ContractViewModel
    {
        public string Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ContractType { get; set; } = string.Empty;
        public decimal Wage { get; set; }
        public string State { get; set; } = string.Empty;
    }
} 