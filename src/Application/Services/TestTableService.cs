using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services;

public class TestTableService : ITestTableService
{
    private readonly ITestTableRepository testTableRepository;
    private readonly IMapper mapper;

    public TestTableService(ITestTableRepository testTableRepository, IMapper mapper)
    {
        this.testTableRepository = testTableRepository;
        this.mapper = mapper;
    }

    public async Task<TestTableDTO> GetTestTableByIdAsync(int id)
    {
        var testTable = await testTableRepository.GetByIdAsync(id);

        if (testTable is null)
        {
            throw new EntityNotFoundException();

        }

        return mapper.Map<TestTableDTO>(testTable);
    }

    public async Task<TestTableDTO> CreateAsync(TestTableCreateDTO testTableCreateDTO)
    {
        var testTable = mapper.Map<TestTable>(testTableCreateDTO);
        var createdTestTable = await testTableRepository.CreateAsync(testTable);

        return mapper.Map<TestTableDTO>(createdTestTable);
    }
}