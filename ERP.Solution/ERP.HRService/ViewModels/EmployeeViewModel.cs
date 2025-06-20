using System;
using System.Collections.Generic;

namespace ERP.HRService.ViewModels
{
    public class EmployeeViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string WorkEmail { get; set; } = string.Empty;
        public string WorkPhone { get; set; } = string.Empty;
        public DateTime? Birthday { get; set; }
        public string Gender { get; set; } = string.Empty;
        public DepartmentViewModel? Department { get; set; }
        public JobViewModel? Job { get; set; }
        public List<ContractViewModel> Contracts { get; set; } = new();
        public List<AttendanceViewModel> Attendances { get; set; } = new();
        public List<LeaveViewModel> Leaves { get; set; } = new();
        public List<EmployeeSkillViewModel> Skills { get; set; } = new();
    }

    public class EmployeeSkillViewModel
    {
        public string SkillId { get; set; }
        public string SkillName { get; set; } = string.Empty;
        public int Level { get; set; }
    }
} 