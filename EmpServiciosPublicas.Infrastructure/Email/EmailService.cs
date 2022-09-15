using EmpServiciosPublicas.Aplication.Contracts.Insfrastructure;
using EmpServiciosPublicas.Aplication.Models;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using modelEmail = EmpServiciosPublicas.Aplication.Models.Email;

namespace EmpServiciosPublicas.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        private EmailSettings EmailSettings { get; }
        private ILogger<EmailService> Logger { get; }

        public EmailService()
        {

        }

        public EmailService(EmailSettings emailSettings, ILogger<EmailService> logger)
        {
            EmailSettings = emailSettings;
            Logger = logger;
        }

        public async Task<bool> SendEmail(modelEmail email)
        {
            var client = new SendGridClient(EmailSettings.ApiKey);

            var subject = email.Subject;
            var to = new EmailAddress(email.To);
            var emailBody = email.Body;

            var from = new EmailAddress
            {
                Email = EmailSettings.FromAddress,
                Name = EmailSettings.FromName
            };

            var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
            var response = await client.SendEmailAsync(sendGridMessage);

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }

            Logger.LogError("El email no pudo enviarse, existen errores");
            return false;
        }
    }
}
