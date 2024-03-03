using Application.DTOs.PortofolioContentDTOs;
using Application.Extensions;
using Application.Interfaces;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services;

public class PortofolioContentService : IPortofolioContentService
{
    private readonly IPortofolioContentRepository portofolioContentRepository;
    private readonly ICurrentUserContextService currentUserContextService;
    private readonly IStorageService storageService;

    public PortofolioContentService(
        IPortofolioContentRepository portofolioContentRepository,
        ICurrentUserContextService currentUserContextService,
        IStorageService storageService
    )
    {
        this.portofolioContentRepository = portofolioContentRepository;
        this.currentUserContextService = currentUserContextService;
        this.storageService = storageService;
    }

    public async Task<PortofolioContentDTO> CreateAsync(
        PortofolioContentCreateDTO portofolioContentCreateDTO
    )
    {
        var portofolioContent = portofolioContentCreateDTO.ToEntity(
            currentUserContextService.GetUserId()
        );

        await portofolioContentRepository.CreateAsync(portofolioContent);

        return portofolioContent.ToDTO();
    }

    public async Task<PortofolioContentDTO> UpdateAsync(
        PortofolioContentUpdateDTO portofolioContentUpdateDTO
    )
    {
        var portofolioContent = await portofolioContentRepository.GetByIdIncludeAllPropertiesAsync(
            portofolioContentUpdateDTO.Id
        );

        if (portofolioContent is null)
        {
            throw new EntityNotFoundException();
        }

        if (portofolioContent.UserId != currentUserContextService.GetUserId())
        {
            throw new UnauthorizedAccessException();
        }

        await portofolioContent.UpdateFromDTOAsync(portofolioContentUpdateDTO, storageService);

        await portofolioContentRepository.UpdateAsync(portofolioContent);

        return portofolioContent.ToDTO();
    }

    public async Task<PortofolioContentGroupDTO> UpdateGroupAsync(
        PortofolioContentGroupUpdateDTO portofolioContentGroupUpdateDTO
    )
    {
        var portofolioContentGroup = new List<PortofolioContentDTO>();

        if (portofolioContentGroupUpdateDTO.PortofolioContents is null)
        {
            throw new ArgumentNullException();
        }

        for (int i = 0; i < portofolioContentGroupUpdateDTO.PortofolioContents.Count; i++)
        {
            var portofolioContent = await UpdateAsync(
                portofolioContentGroupUpdateDTO.PortofolioContents[i]
            );
            portofolioContentGroup.Add(portofolioContent);
        }

        return new PortofolioContentGroupDTO { PortofolioContents = portofolioContentGroup };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var portofolioContent = await portofolioContentRepository.GetByIdIncludeAllPropertiesAsync(id);

        if (portofolioContent is null)
        {
            throw new EntityNotFoundException();
        }

        if (portofolioContent.UserId != currentUserContextService.GetUserId())
        {
            throw new UnauthorizedAccessException();
        }

        foreach (var media in portofolioContent.Medias)
        {
            var fileName = Path.GetFileName(new Uri(media.Uri).LocalPath);
            await storageService.DeleteFile(fileName, "portofoliocontents");
        }

        await portofolioContentRepository.DeleteAsync(portofolioContent);

        return true;
    }
}
