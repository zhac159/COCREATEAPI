using Application.DTOs.ExperienceDTOs;

namespace Application.Interfaces;

public interface IExperienceService
{
    Task<ExperienceDTO> CreateAsync(ExperienceCreateDTO experienceCreateDTO);

}