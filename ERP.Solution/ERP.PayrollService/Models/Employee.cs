namespace ERP.PayrollService.Models
{
    /// <summary>
    /// Represents an employee in the organization.
    /// </summary>
    public class Employee
    {
        
        public int Id { get; set; }
        /// <example>John</example>
        public string FirstName { get; set; }
        /// <example>Doe</example>
        public string LastName { get; set; }
        /// <example>john.doe@example.com</example>
        public string Email { get; set; }
        /// <example>MP Nagar</example>
        public string Address { get; set; }
        /// <example>Information Technology</example>
        public string Department { get; set; }
        /// <example>Software Engineer</example>
        public string Position { get; set; }
        /// <example>50000</example>
        public decimal BaseSalary { get; set; }
        /// <example>John Doe</example>
        public string Name { get; set; }
        public ICollection<EmployeeContract> EmployeeContracts { get; set; }
        public ICollection<Leave> Leaves { get; set; }
        public ICollection<Loan> Loans { get; set; }

    }
} 