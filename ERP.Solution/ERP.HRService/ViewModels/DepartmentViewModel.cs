using System.Collections.Generic;

namespace ERP.HRService.ViewModels
{
    public class DepartmentViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DepartmentViewModel? ParentDepartment { get; set; }
        public List<DepartmentViewModel> ChildDepartments { get; set; } = new();
    }
} 