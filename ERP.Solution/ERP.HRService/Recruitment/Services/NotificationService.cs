using System;
using System.Threading.Tasks;
using ERP.Recruitment.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Linq;
using ERP.HRService.Recruitment.Data;

namespace ERP.Recruitment.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<NotificationService> _logger;
        private readonly IConfiguration _configuration;
        private readonly RecruitmentDbContext _dbContext;

        public NotificationService(
            IEmailService emailService,
            ILogger<NotificationService> logger,
            IConfiguration configuration,
            RecruitmentDbContext dbContext)
        {
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task SendApplicationNotificationAsync(Guid applicationId, string recipientEmail, string subject, string message)
        {
            try
            {
                await _emailService.SendEmailAsync(recipientEmail, subject, message);
                _logger.LogInformation($"Application notification sent for application {applicationId} to {recipientEmail}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send application notification for application {applicationId}");
                throw;
            }
        }

        public async Task SendJobPositionUpdateAsync(Guid jobPositionId, string recipientEmail, string subject, string message)
        {
            try
            {
                await _emailService.SendEmailAsync(recipientEmail, subject, message);
                _logger.LogInformation($"Job position update notification sent for position {jobPositionId} to {recipientEmail}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send job position update notification for position {jobPositionId}");
                throw;
            }
        }

        public async Task SendInterviewNotificationAsync(Guid interviewId, string recipientEmail, DateTime interviewDateTime, string location)
        {
            try
            {
                var subject = "Interview Scheduled";
                var message = $"Your interview has been scheduled for {interviewDateTime:g} at {location}";
                await _emailService.SendEmailAsync(recipientEmail, subject, message);
                _logger.LogInformation($"Interview notification sent for interview {interviewId} to {recipientEmail}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send interview notification for interview {interviewId}");
                throw;
            }
        }

        public async Task SendStatusUpdateNotificationAsync(Guid entityId, string recipientEmail, string status, string message)
        {
            try
            {
                var subject = $"Status Update: {status}";
                await _emailService.SendEmailAsync(recipientEmail, subject, message);
                _logger.LogInformation($"Status update notification sent for entity {entityId} to {recipientEmail}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send status update notification for entity {entityId}");
                throw;
            }
        }

        public Task SendDepartmentNotificationAsync(string departmentId, string subject, string message)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetDepartmentHeadEmailAsync(string department)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetHREmailAsync()
        {
            throw new NotImplementedException();
        }

        public Task CreateNotificationAsync(string title, string message, string[] recipients)
        {
            throw new NotImplementedException();
        }
    }
} 