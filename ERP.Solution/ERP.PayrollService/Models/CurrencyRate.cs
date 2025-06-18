using System;

namespace ERP.PayrollService.Models
{   /// <summary>
    /// Represents a currency exchange rate used in payroll conversions.
    /// </summary>
    public class CurrencyRate
    {
        public int Id { get; set; }
        /// <example>USD</example>
        public string FromCurrency { get; set; }
        /// <example>INR</example>
        public string ToCurrency { get; set; }
        /// <example>83.25</example>
        public decimal Rate { get; set; }
        /// <example>2024-06-18T00:00:00</example>
        public DateTime Date { get; set; }
    }
} 