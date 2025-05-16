using System;
using ERP.Recruitment.Models;

namespace ERP.Recruitment.DTOs
{
    public class JobApplicationDto
    {
        public int Id { get; set; }
        public int JobPositionId { get; set; }
        public string JobPositionName { get; set; }
        public string ApplicantName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ResumeUrl { get; set; }
        public string CoverLetter { get; set; }
        public ApplicationStatus Status { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string CurrentStage { get; set; }
        public string Notes { get; set; }
        public decimal? ExpectedSalary { get; set; }
        public string Source { get; set; }
    }

    public class CreateJobApplicationDto
    {
        public int JobPositionId { get; set; }
        public string ApplicantName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ResumeUrl { get; set; }
        public string CoverLetter { get; set; }
        public decimal? ExpectedSalary { get; set; }
        public string Source { get; set; }
    }

    public class UpdateJobApplicationDto
    {
        public ApplicationStatus Status { get; set; }
        public string CurrentStage { get; set; }
        public string Notes { get; set; }
    }
} 