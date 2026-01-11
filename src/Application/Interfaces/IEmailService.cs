using Application.DTOs.EmailDTOs;

namespace Application.Interfaces;

public interface IEmailService
{
    Task SendEmail(SendEmailDTO sendEmailDTO);

    Task SendAndCacheEmailVerificationAsync(string emailAddress, int userId );
}
