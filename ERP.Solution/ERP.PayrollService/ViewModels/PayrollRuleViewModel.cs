namespace ERP.PayrollService.ViewModels
{
    /// <summary>
    /// Represents the view model for a payroll rule, 
    /// including its name, formula, and execution sequence.
    /// </summary>
    public class PayrollRuleViewModel
    {
        /// <example>1</example>
        public int Id { get; set; }

        /// <example>HRA</example>
        public string Name { get; set; }

        /// <example>BaseSalary * 0.40</example>
        public string Formula { get; set; }

        /// <example>2</example>
        public int Sequence { get; set; }
    }
}
