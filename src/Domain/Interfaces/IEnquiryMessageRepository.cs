using Domain.Entities;

namespace Domain.Interfaces;

public interface IEnquiryMessageRepository
{
    Task<EnquiryMessage> CreateAsync(EnquiryMessage enquiryMessage);
    Task<EnquiryMessage?> GetByIdAsync(int id);
    Task<bool> DeleteAsync(EnquiryMessage enquiryMessage);
}
