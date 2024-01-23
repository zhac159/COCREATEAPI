using Application.DTOs.PortofolioContentDTOs;

namespace Application.Interfaces;

public interface IPortofolioContentService
{
    Task<PortofolioContentDTO> CreateAsync(PortofolioContentCreateWrapperDTO portofolioContentCreateWrapperDTO, int userId);
    Task<bool> DeleteAsync(int id, int userId); 
    Task<PortofolioContentDTO> UpdateAsync(PortofolioContentUpdateWrapperDTO portofolioContentUpdateWrapperDTO, int userId);
}