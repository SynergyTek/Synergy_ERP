using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ERP.Recruitment.Models
{
    public class RecruitmentSource
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public decimal? CostPerHire { get; set; }

        public string ExternalLink { get; set; }

        public DateTime CreatedDate { get; set; }

        // Statistics
        public int TotalApplications { get; set; }
        public int SuccessfulHires { get; set; }

        // Navigation property
        public virtual ICollection<JobApplication> Applications { get; set; }
    }
} 