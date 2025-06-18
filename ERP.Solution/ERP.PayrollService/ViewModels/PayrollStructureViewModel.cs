using ERP.PayrollService.ViewModels;
using System.Collections.Generic;

namespace ERP.PayrollService.ViewModels
{
    /// <summary>
    /// Represents the view model for a payroll structure,
    /// including rules, allowances, and deductions.
    /// </summary>
    public class PayrollStructureViewModel
    {
        /// <example>1</example>
        public int Id { get; set; }

        /// <example>Standard Payroll Structure</example>
        public string Name { get; set; }

        /// <example>Applies to all full-time employees</example>
        public string Description { get; set; }

        public List<PayrollRuleViewModel> Rules { get; set; }

        public List<PayrollRuleViewModel> Allowances { get; set; }

        public List<DeductionViewModel> Deductions { get; set; }
    }
}
