using System;
using System.Net.Mail;
using System.Threading.Tasks;
using ERP.Recruitment.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace ERP.Recruitment.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;
        private readonly SmtpClient _smtpClient;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            var smtpConfig = _configuration.GetSection("Smtp");
            _smtpClient = new SmtpClient
            {
                Host = smtpConfig["Host"],
                Port = int.Parse(smtpConfig["Port"]),
                EnableSsl = bool.Parse(smtpConfig["EnableSsl"]),
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(
                    smtpConfig["Username"],
                    smtpConfig["Password"]
                )
            };
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                var message = new MailMessage
                {
                    From = new MailAddress(_configuration["Smtp:FromAddress"]),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                message.To.Add(to);

                await _smtpClient.SendMailAsync(message);
                _logger.LogInformation($"Email sent successfully to {to}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send email to {to}");
                throw;
            }
        }

        public async Task SendTemplateEmailAsync(string to, string templateName, object templateData)
        {
            try
            {
                // In a real implementation, you would load the template from a template engine
                // For now, we'll use a simple JSON serialization of the template data
                var jsonData = JsonSerializer.Serialize(templateData, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                var body = $@"
                    <h2>Recruitment Notification</h2>
                    <p>Template: {templateName}</p>
                    <pre>{jsonData}</pre>
                ";

                await SendEmailAsync(to, $"Recruitment: {templateName}", body);
                _logger.LogInformation($"Template email '{templateName}' sent successfully to {to}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send template email '{templateName}' to {to}");
                throw;
            }
        }
    }
} 