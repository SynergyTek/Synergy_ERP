using ERP.PayrollService.ViewModels;
using System.Collections.Generic;

namespace ERP.PayrollService.ViewModels
{
    public class PayrollStructureViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<PayrollRuleViewModel> Rules { get; set; }
        public List<PayrollRuleViewModel> Allowances { get; set; }
        public List<DeductionViewModel> Deductions { get; set; }
    }
} 