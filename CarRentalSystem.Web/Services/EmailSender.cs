using System.Net;
using System.Net.Mail;
using CarRentalSystem.Core.Interfaces;
using CarRentalSystem.Web.Interfaces;
using CarRentalSystem.Web.Settings;
using Microsoft.Extensions.Options;

namespace CarRentalSystem.Web.Services;

public class EmailSender : IEmailSender
{
    private readonly SmtpSettings _smtpSettings;
    private readonly ILogger<EmailSender> _logger;

    public EmailSender(
        IOptions<SmtpSettings> smtpSettings,
        ILogger<EmailSender> logger)
    {
        _smtpSettings = smtpSettings.Value;
        _logger = logger;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var client = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port)
        {
            Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
            EnableSsl = true
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_smtpSettings.From),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true
        };
        mailMessage.To.Add(email);

        try
        {
            await client.SendMailAsync(mailMessage);
            _logger.LogInformation("Email sent to {Email} successfully.", email);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending email to {Email}.", email);
            throw;
        }
    }
}