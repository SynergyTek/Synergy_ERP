using System;
using System.Collections.Generic;

namespace ERP.PayrollService.ViewModels
{
    /// <summary>
    /// Represents a summary view of a payslip including payroll structure, employee, and pay details.
    /// </summary>
    public class PayslipViewModel
    {
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// Details of the employee for whom the payslip is generated.
        /// </summary>
        public EmployeeViewModel Employee { get; set; }

        /// <summary>
        /// The payroll structure used for calculating this payslip.
        /// </summary>
        public PayrollStructureViewModel PayrollStructure { get; set; }

        /// <example>2025-05-01T00:00:00</example>
        public DateTime PeriodStart { get; set; }

        /// <example>2025-05-31T00:00:00</example>
        public DateTime PeriodEnd { get; set; }

        /// <example>60000.00</example>
        public decimal GrossPay { get; set; }

        /// <example>50000.00</example>
        public decimal NetPay { get; set; }

        /// <summary>
        /// List of payslip line items like allowances or deductions.
        /// </summary>
        public List<PayslipLineViewModel> PayslipLines { get; set; }

        /// <summary>
        /// Batch to which the payslip belongs.
        /// </summary>
        public PayrollBatchViewModel PayrollBatch { get; set; }
    }
}
