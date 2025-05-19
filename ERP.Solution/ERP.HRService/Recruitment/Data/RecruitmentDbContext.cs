using ERP.HRService.Recruitment.Models;
using Microsoft.EntityFrameworkCore;

namespace ERP.HRService.Recruitment.Data
{
    public class RecruitmentDbContext(DbContextOptions<RecruitmentDbContext> options) : DbContext(options)
    {
        public DbSet<JobPosition> JobPositions { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<JobPositionStage> JobPositionStages { get; set; }
        public DbSet<JobSurvey> JobSurveys { get; set; }
        public DbSet<JobSurveyQuestion> JobSurveyQuestions { get; set; }
        public DbSet<SalaryPackage> SalaryPackages { get; set; }
        public DbSet<RecruitmentSource> RecruitmentSources { get; set; }
        public DbSet<Interview> Interviews { get; set; }
        public DbSet<InterviewFeedback> InterviewFeedbacks { get; set; }
        public DbSet<InterviewParticipant> InterviewParticipants { get; set; }
        public DbSet<RecruitmentCampaign> RecruitmentCampaigns { get; set; }
        public DbSet<CampaignActivity> CampaignActivities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // // Configure JobPosition
            // modelBuilder.Entity<JobPosition>()
            //     .HasMany(j => j.JobApplications)
            //     .WithOne(a => a.JobPosition)
            //     .HasForeignKey(a => a.JobPositionId)
            //     .OnDelete(DeleteBehavior.Cascade);

            // Add indexes
            modelBuilder.Entity<JobPosition>()
                .HasIndex(j => j.Name);

            modelBuilder.Entity<JobApplication>()
                .HasIndex(a => a.Email);

            modelBuilder.Entity<JobApplication>()
                .HasIndex(a => a.Status);

            // Configure JobSurvey relationships
            modelBuilder.Entity<JobSurvey>()
                .HasMany(s => s.Questions)
                .WithOne(q => q.Survey)
                .HasForeignKey(q => q.JobSurveyId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure SalaryPackage
            modelBuilder.Entity<JobApplication>()
                .HasOne(s => s.JobPosition)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            // Configure JobPositionStage
            modelBuilder.Entity<JobPositionStage>()
                .HasIndex(s => s.Sequence);

            // New configurations
            modelBuilder.Entity<Interview>()
                .HasOne(i => i.JobApplication)
                .WithMany()
                .HasForeignKey(i => i.JobApplicationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<InterviewFeedback>()
                .HasOne(f => f.Interview)
                .WithMany(i => i.Feedbacks)
                .HasForeignKey(f => f.InterviewId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<InterviewParticipant>()
                .HasOne(p => p.Interview)
                .WithMany(i => i.Participants)
                .HasForeignKey(p => p.InterviewId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RecruitmentSource>()
                .HasMany(s => s.Applications)
                .WithOne()
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<RecruitmentCampaign>()
                .HasMany(c => c.JobPositions)
                .WithMany();

            modelBuilder.Entity<RecruitmentCampaign>()
                .HasMany(c => c.Sources)
                .WithMany();

            modelBuilder.Entity<CampaignActivity>()
                .HasOne(a => a.Campaign)
                .WithMany(c => c.Activities)
                .HasForeignKey(a => a.CampaignId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes for performance
            modelBuilder.Entity<Interview>()
                .HasIndex(i => i.ScheduledDate);

            modelBuilder.Entity<RecruitmentCampaign>()
                .HasIndex(c => c.StartDate);

            modelBuilder.Entity<RecruitmentSource>()
                .HasIndex(s => s.Name)
                .IsUnique();
        }
    }
} 