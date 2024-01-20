using API.Factories;
using API.Models;
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class TestTable : COCREATEAPIControllerBase
{

    private readonly ITestTableService testTableService;

    public TestTable(ITestTableService testTableService)
    {
        this.testTableService = testTableService;
    }

    [HttpPost("create")]
    public async Task<ActionResult<APIResponse<TestTableDTO>>> Post(TestTableCreateDTO testTableCreateDTO)
    {
        var createdTestTable = await testTableService.CreateAsync(testTableCreateDTO);
        return Ok(APIResponseFactory.CreateSuccess(createdTestTable));
    }
}