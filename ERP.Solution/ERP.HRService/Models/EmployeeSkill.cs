namespace ERP.HRService.Models
{
    public class EmployeeSkill
    {
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;
        public string SkillId { get; set; }
        public Skill Skill { get; set; } = null!;
        public int Level { get; set; } // 1-5 or similar
    }
} 