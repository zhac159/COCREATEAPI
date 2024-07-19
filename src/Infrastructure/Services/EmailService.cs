using System.Net.Mail;
using Application.DTOs.EmailDTOs;
using Application.Interfaces;
using Domain.Interfaces;

namespace Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IMessageStorageService messageStorageService;


    public EmailService(
        IMessageStorageService messageStorageService
    )
    {
        this.messageStorageService = messageStorageService;
    }

    public async Task SendEmail(SendEmailDTO sendEmailDTO)
    {
        var outlookAddress = "cocreatetest01@outlook.com";
        var outlookPassword = "Abitha2001!";

        var mailMessage = new MailMessage
        {
            From = new MailAddress(outlookAddress),
            Subject = sendEmailDTO.Subject,
            Body = sendEmailDTO.Body,
            IsBodyHtml = true
        };

        mailMessage.To.Add("abitha302001@gmail.com");

        var outlookClient = new SmtpClient
        {
            Host = "smtp.office365.com",
            Port = 587,
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new System.Net.NetworkCredential(
                outlookAddress,
                outlookPassword,
                "outlook.com"
            )
        };

        await outlookClient.SendMailAsync(mailMessage);
    }

    public async Task SendAndCacheEmailVerificationAsync(string emailAddress, int userId)
    {

        var emailVerificationToken = new Random().Next(10000, 99999).ToString();

        await SendEmail(
            new SendEmailDTO
            {
                To = emailAddress,
                Subject = "Welcome to the platform",
                Body = "The verification token is: " + emailVerificationToken + "."
            }
        );

        await messageStorageService.StoreOneTimeEmailTokenAsync(emailVerificationToken, userId);
    }
}
