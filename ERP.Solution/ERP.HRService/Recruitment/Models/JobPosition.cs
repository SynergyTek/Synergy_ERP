using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ERP.Recruitment.Models
{
    public class JobPosition
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string JobDescription { get; set; }

        public int NumberOfVacancies { get; set; }

        public DateTime ExpectedClosingDate { get; set; }

        public bool IsActive { get; set; }

        public string Requirements { get; set; }

        public string Responsibilities { get; set; }

        public decimal MinSalary { get; set; }

        public decimal MaxSalary { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        // Navigation properties
        public virtual ICollection<JobApplication> JobApplications { get; set; }
    }
} 