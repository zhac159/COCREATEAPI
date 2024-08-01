using System.Net.Mail;
using Application.DTOs.EmailDTOs;
using Application.Interfaces;
using Azure;
using Azure.Communication.Email;
using Domain.Interfaces;

namespace Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IMessageStorageService messageStorageService;

    public EmailService(IMessageStorageService messageStorageService)
    {
        this.messageStorageService = messageStorageService;
    }

    public async Task SendEmail(SendEmailDTO sendEmailDTO)
    {
        string connectionString =
            "endpoint=https://wecreatexcommunicationresource.uk.communication.azure.com/;accesskey=5bnYU4fjlgr7yQoZRh5Cz0K8RLw5UwQeoZfLmUO8g3455kxCg7QZJQQJ99AGACULyCpAHQGDAAAAAZCSM5Um";

        var emailClient = new EmailClient(connectionString);

        await emailClient.SendAsync(
            WaitUntil.Completed,
            senderAddress: "DoNotReply@f1603040-002a-45ea-8c12-cff92bdf3c8d.azurecomm.net",
            recipientAddress: sendEmailDTO.To,
            subject: sendEmailDTO.Subject,
            htmlContent: "<html><h1>This is your confirmation code:"
                + sendEmailDTO.Body
                + "</h1l></html>",
            plainTextContent: "This is your confirmation code: " + sendEmailDTO.Body
        );
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
