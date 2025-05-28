namespace ERP.PayrollService.Models
{
    public class Employee
    {
        
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }
        public string Position { get; internal set; }
        public decimal BaseSalary { get; set; }
        public string Name { get; set; }

    }
} 