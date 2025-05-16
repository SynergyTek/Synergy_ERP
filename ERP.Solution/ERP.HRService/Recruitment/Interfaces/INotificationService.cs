using System;
using System.Threading.Tasks;

namespace ERP.Recruitment.Interfaces
{
    public interface INotificationService
    {
        Task SendApplicationNotificationAsync(Guid applicationId, string recipientEmail, string subject, string message);
        Task SendJobPositionUpdateAsync(Guid jobPositionId, string recipientEmail, string subject, string message);
        Task SendInterviewNotificationAsync(Guid interviewId, string recipientEmail, DateTime interviewDateTime, string location);
        Task SendStatusUpdateNotificationAsync(Guid entityId, string recipientEmail, string status, string message);
        Task SendDepartmentNotificationAsync(string departmentId, string subject, string message);
        Task<string> GetDepartmentHeadEmailAsync(string department);
        Task<string> GetHREmailAsync();
        Task CreateNotificationAsync(string title, string message, string[] recipients);
    }
} 