using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ERP.Recruitment.Models
{
    public class Interview
    {
        [Key]
        public int Id { get; set; }

        public int JobApplicationId { get; set; }

        [Required]
        public DateTime ScheduledDate { get; set; }

        public DateTime? ActualStartTime { get; set; }

        public DateTime? ActualEndTime { get; set; }

        [Required]
        public InterviewType Type { get; set; }

        public string Location { get; set; }

        public string MeetingLink { get; set; }

        public InterviewStatus Status { get; set; }

        public string Notes { get; set; }

        // Navigation properties
        public virtual JobApplication JobApplication { get; set; }
        public virtual ICollection<InterviewFeedback> Feedbacks { get; set; }
        public virtual ICollection<InterviewParticipant> Participants { get; set; }
    }

    public class InterviewFeedback
    {
        [Key]
        public int Id { get; set; }

        public int InterviewId { get; set; }

        [Required]
        public string InterviewerId { get; set; }

        public int TechnicalScore { get; set; }

        public int CommunicationScore { get; set; }

        public int ExperienceScore { get; set; }

        public int CulturalFitScore { get; set; }

        [Required]
        public string Comments { get; set; }

        public string Strengths { get; set; }

        public string AreasForImprovement { get; set; }

        public bool IsRecommended { get; set; }

        public DateTime CreatedDate { get; set; }

        // Navigation property
        public virtual Interview Interview { get; set; }
    }

    public class InterviewParticipant
    {
        [Key]
        public int Id { get; set; }

        public int InterviewId { get; set; }

        [Required]
        public string ParticipantId { get; set; }

        [Required]
        public ParticipantRole Role { get; set; }

        public bool HasConfirmed { get; set; }

        public string Notes { get; set; }

        // Navigation property
        public virtual Interview Interview { get; set; }
    }

    public enum InterviewType
    {
        Phone,
        Video,
        InPerson,
        Technical,
        HR,
        Final
    }

    public enum InterviewStatus
    {
        Scheduled,
        Confirmed,
        InProgress,
        Completed,
        Cancelled,
        Rescheduled,
        NoShow
    }

    public enum ParticipantRole
    {
        Interviewer,
        HiringManager,
        TechnicalExpert,
        HRRepresentative,
        DepartmentHead
    }
} 