using System;
using System.Collections.Generic;

namespace ERP.HRService.Models
{
    public class Employee
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string WorkEmail { get; set; } = string.Empty;
        public string WorkPhone { get; set; } = string.Empty;
        public DateTime? Birthday { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public string? JobId { get; set; }
        public Job? Job { get; set; }
        public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
        public ICollection<Leave> Leaves { get; set; } = new List<Leave>();
        public ICollection<EmployeeSkill> EmployeeSkills { get; set; } = new List<EmployeeSkill>();
        public DateTime JoiningDate { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
    }
} 