using API.Controllers;
using Application.DTOs.ExperienceDTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class ExperienceController : COCREATEAPIControllerBase
{
    private readonly IExperienceService experienceService;

    public ExperienceController(IExperienceService experienceService)
    {
        this.experienceService = experienceService;
    }

    [HttpPost]
    public async Task<ActionResult<ExperienceDTO>> Create(ExperienceCreateDTO experienceCreateDTO)
    {
        var createdExperience = await experienceService.CreateAsync(experienceCreateDTO);

        return Ok(createdExperience);
    }
}