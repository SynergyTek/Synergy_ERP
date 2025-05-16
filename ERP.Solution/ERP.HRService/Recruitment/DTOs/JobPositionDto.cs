using System;

namespace ERP.Recruitment.DTOs
{
    public class JobPositionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string JobDescription { get; set; }
        public int NumberOfVacancies { get; set; }
        public DateTime ExpectedClosingDate { get; set; }
        public bool IsActive { get; set; }
        public string Requirements { get; set; }
        public string Responsibilities { get; set; }
        public decimal MinSalary { get; set; }
        public decimal MaxSalary { get; set; }
    }

    public class CreateJobPositionDto
    {
        public string Name { get; set; }
        public string Department { get; set; }
        public string JobDescription { get; set; }
        public int NumberOfVacancies { get; set; }
        public DateTime ExpectedClosingDate { get; set; }
        public string Requirements { get; set; }
        public string Responsibilities { get; set; }
        public decimal MinSalary { get; set; }
        public decimal MaxSalary { get; set; }
    }

    public class UpdateJobPositionDto : CreateJobPositionDto
    {
        public bool IsActive { get; set; }
    }
} 