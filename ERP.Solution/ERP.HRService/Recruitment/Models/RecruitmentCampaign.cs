using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Recruitment.Models
{
    public class RecruitmentCampaign
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsActive { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Budget { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SpentAmount { get; set; }

        public int TargetHires { get; set; }

        public int ActualHires { get; set; }

        public string TargetDepartments { get; set; } // Comma-separated list

        public string MarketingMaterials { get; set; } // JSON array of URLs

        public string SuccessMetrics { get; set; } // JSON object

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        // Navigation properties
        public virtual ICollection<JobPosition> JobPositions { get; set; }
        public virtual ICollection<RecruitmentSource> Sources { get; set; }
        public virtual ICollection<CampaignActivity> Activities { get; set; }
    }

    public class CampaignActivity
    {
        [Key]
        public int Id { get; set; }

        public int CampaignId { get; set; }

        [Required]
        [StringLength(100)]
        public string ActivityName { get; set; }

        public string Description { get; set; }

        public DateTime ScheduledDate { get; set; }

        public ActivityType Type { get; set; }

        public ActivityStatus Status { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Cost { get; set; }

        public string Result { get; set; }

        public DateTime CreatedDate { get; set; }

        // Navigation property
        public virtual RecruitmentCampaign Campaign { get; set; }
    }

    public enum ActivityType
    {
        JobFair,
        UniversityEvent,
        SocialMediaCampaign,
        ReferralDrive,
        Workshop,
        Webinar,
        Advertisement,
        Other
    }

    public enum ActivityStatus
    {
        Planned,
        InProgress,
        Completed,
        Cancelled,
        Postponed
    }
} 