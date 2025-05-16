using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ERP.Recruitment.Models
{
    public class JobSurvey
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        // Navigation property
        public virtual ICollection<JobSurveyQuestion> Questions { get; set; }
    }

    public class JobSurveyQuestion
    {
        [Key]
        public int Id { get; set; }

        public int JobSurveyId { get; set; }

        [Required]
        public string Question { get; set; }

        [Required]
        public QuestionType Type { get; set; }

        public string Options { get; set; } // JSON array for multiple choice

        public bool IsRequired { get; set; }

        public int Sequence { get; set; }

        // Navigation property
        public virtual JobSurvey Survey { get; set; }
    }

    public enum QuestionType
    {
        Text,
        MultipleChoice,
        Checkbox,
        Date,
        Number,
        File
    }
} 