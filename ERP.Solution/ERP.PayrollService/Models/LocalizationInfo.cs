namespace ERP.PayrollService.Models
{
    /// <summary>
    /// Represents localization details such as country-specific payroll or HR configurations.
    /// </summary>
    public class LocalizationInfo
    {
        /// <example>1</example>
        public int Id { get; set; }

        /// <example>India</example>
        public string Country { get; set; }

        /// <example>Includes statutory tax and leave rules for India</example>
        public string Description { get; set; }
    }
}
