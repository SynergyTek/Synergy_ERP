using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Recruitment.Models
{
    public class JobApplication
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("JobPosition")]
        public int JobPositionId { get; set; }

        [Required]
        [StringLength(100)]
        public string ApplicantName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }

        public string ResumeUrl { get; set; }

        public string CoverLetter { get; set; }

        [Required]
        public ApplicationStatus Status { get; set; }

        public DateTime ApplicationDate { get; set; }

        public DateTime? LastStatusUpdateDate { get; set; }

        public string CurrentStage { get; set; }

        public string Notes { get; set; }

        public decimal? ExpectedSalary { get; set; }

        public string Source { get; set; }

        // Navigation property
        public virtual JobPosition JobPosition { get; set; }
    }

    public enum ApplicationStatus
    {
        New,
        UnderReview,
        Shortlisted,
        InterviewScheduled,
        InterviewCompleted,
        Selected,
        Rejected,
        OnHold,
        Withdrawn
    }
} 