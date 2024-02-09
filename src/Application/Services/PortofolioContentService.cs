using System.Text;
using Application.DTOs.PortofolioContentDTOs;
using Application.Extensions;
using Application.Interfaces;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services;

public class PortofolioContentService : IPortofolioContentService
{
    private readonly IPortofolioContentRepository portofolioContentRepository;
    private readonly IStorageService storageService;

    public PortofolioContentService(IPortofolioContentRepository portofolioContentRepository, IStorageService storageService)
    {
        this.portofolioContentRepository = portofolioContentRepository;
        this.storageService = storageService;
    }
    
        public async Task<PortofolioContentDTO> CreateAsync(PortofolioContentCreateWrapperDTO portofolioContentCreateWrapperDTO, int userId)
    {
        var fileSrc = new StringBuilder()
            .Append("portofoliocontent_")
            .Append(userId)
            .Append("_")
            .Append(portofolioContentCreateWrapperDTO.PortofolioContent.Name)
            .Append("_")
            .Append(Guid.NewGuid().ToString())
            .Append(".jpeg")
            .ToString();

        var mediaFile = portofolioContentCreateWrapperDTO.MediaFile;

        await storageService.UploadFile(fileSrc, mediaFile, "portofoliocontents");

        var portofolioContent = portofolioContentCreateWrapperDTO.PortofolioContent.ToEntity(fileSrc, userId);

        var createdPortofolioContent = await portofolioContentRepository.CreateAsync(portofolioContent);

        return createdPortofolioContent.ToDTO();
    }

        public async Task<bool> DeleteAsync(int id, int userId)
    {
        var portofolioContent = await portofolioContentRepository.GetByIdAsync(id);

        if (portofolioContent is null)
        {
            throw new EntityNotFoundException();
        }

        await storageService.DeleteFile(portofolioContent.FileSrc, "portofoliocontents");

        await portofolioContentRepository.DeleteAsync(portofolioContent);

        return true;
    }

    public async Task<PortofolioContentDTO> UpdateAsync(PortofolioContentUpdateWrapperDTO portofolioContentUpdateWrapperDTO, int userId)
    {
        var portofolioContent = await portofolioContentRepository.GetByIdAsync(portofolioContentUpdateWrapperDTO.PortofolioContentUpdateDTO.Id);

        if (portofolioContent is null)
        {
            throw new EntityNotFoundException();
        }

        if (portofolioContentUpdateWrapperDTO.MediaFile is not null)
        {

            await storageService.UploadFile(portofolioContent.FileSrc, portofolioContentUpdateWrapperDTO.MediaFile, "portofolioContents");
        }

        portofolioContent.UpdateFromDTO(portofolioContentUpdateWrapperDTO.PortofolioContentUpdateDTO);

        await portofolioContentRepository.UpdateAsync(portofolioContent);

        return portofolioContent.ToDTO();
    }
}