using Domain.Entities;

namespace Domain.Interfaces;

public interface IPortofolioContentRepository
{
    Task<PortofolioContent?> GetByIdAsync(int id);
    Task<PortofolioContent> CreateAsync(PortofolioContent asset);
    Task<PortofolioContent> UpdateAsync(PortofolioContent asset);
    Task<bool> DeleteAsync(PortofolioContent asset);
}
