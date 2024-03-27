using Application.DTOs.PortofolioContentDTOs;

namespace Application.Interfaces;

public interface IPortofolioContentService
{
    Task<PortofolioContentDTO> CreateAsync(PortofolioContentCreateDTO portofolioContentCreateDTO);
    Task<bool> DeleteAsync(int id); 
    Task<PortofolioContentDTO> UpdateAsync(PortofolioContentUpdateDTO portofolioContentUpdateDTO);

    Task<PortofolioContentGroupDTO> UpdateGroupAsync(PortofolioContentGroupUpdateDTO portofolioContentGroupUpdateDTO);

}