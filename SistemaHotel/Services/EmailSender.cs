using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace SistemaHotel.Services;

public class EmailSender : IEmailSender
{
    private readonly SmtpClient _client;
    private readonly string _username;

    public EmailSender(string host, int port, bool useSsl, string username, string password)
    {
        _client = new SmtpClient(host)
        {
            Port = port,
            EnableSsl = useSsl,
            Credentials = new NetworkCredential(username, password)
        };
        _username = username;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var message = new MailMessage
        {
            From = new MailAddress(_username),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true
        };

        message.To.Add(email);

        await _client.SendMailAsync(message);
    }
}