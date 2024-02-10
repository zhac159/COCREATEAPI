using Domain.Entities;

namespace Domain.Interfaces;

public interface IEnquiryRepository
{
    Task<Enquiry?> GetByIdAsync(int id);
    Task<Enquiry> CreateAsync(Enquiry enquiry);
    Task<Enquiry> UpdateAsync(Enquiry enquiry);
    Task<bool> DeleteAsync(Enquiry enquiry);
}