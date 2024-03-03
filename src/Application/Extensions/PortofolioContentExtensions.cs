using Application.DTOs.PortofolioContentDTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Extensions;

public static class PortofolioContentExtensions
{
    public static PortofolioContentDTO ToDTO(this PortofolioContent portofolioContent)
    {
        return new PortofolioContentDTO
        {
            Id = portofolioContent.Id,
            Description = portofolioContent.Description,
            SkillType = portofolioContent.SkillType,
            Medias = portofolioContent
                .Medias.OrderBy(media => media.Order)
                .Select(x => x.ToDTO())
                .ToList()
        };
    }

    public static async Task UpdateFromDTOAsync(
        this PortofolioContent portofolioContent,
        PortofolioContentUpdateDTO portofolioContentUpdateDTO,
        IStorageService storageService
    )
    {
        portofolioContent.Description =
            portofolioContentUpdateDTO.Description ?? portofolioContent.Description;
        portofolioContent.SkillType =
            portofolioContentUpdateDTO.SkillType ?? portofolioContent.SkillType;
        portofolioContent.Order = portofolioContentUpdateDTO.Order ?? portofolioContent.Order;

        if (portofolioContentUpdateDTO.Medias is not null)
        {
            var updateMediaIds = portofolioContentUpdateDTO.Medias.Select(m => m.Id).ToList();

            var mediasToDelete = portofolioContent
                .Medias.Where(m => !updateMediaIds.Contains(m.Id))
                .ToList();

            foreach (var media in mediasToDelete)
            {
                var fileName = Path.GetFileName(new Uri(media.Uri).LocalPath);

                await storageService.DeleteFile(fileName, "portofoliocontents");
            }

            foreach (var updatedMedia in portofolioContentUpdateDTO.Medias)
            {
                var originalMedia = portofolioContent.Medias.FirstOrDefault(
                    m => m.Id == updatedMedia.Id
                );

                if (originalMedia != null && originalMedia.Uri != updatedMedia.Uri)
                {
                    Console.WriteLine(originalMedia.Uri);
                    var fileName = Path.GetFileName(new Uri(originalMedia.Uri).LocalPath);
                    await storageService.DeleteFile(fileName, "portofoliocontents");
                }
            }

            portofolioContent.Medias?.RemoveAll(
                m => !portofolioContentUpdateDTO.Medias.Any(mu => mu.Id == m.Id)
            );

            portofolioContent.Medias = portofolioContentUpdateDTO
                .Medias.Select(
                    (mediaUpdateDTO, order) =>
                    {
                        var media = portofolioContent.Medias?.FirstOrDefault(
                            m => m.Id == mediaUpdateDTO.Id
                        );
                        if (media is not null)
                        {
                            media.UpdateFromDTO(mediaUpdateDTO, order);
                            return media;
                        }
                        return mediaUpdateDTO.ToPortofolioContentMediaEntity(order);
                    }
                )
                .ToList();
        }
    }
}
