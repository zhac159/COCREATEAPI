using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles;

public class TestTableProfile : Profile
{
    public TestTableProfile()
    {
        CreateMap<TestTable, TestTableDTO>();

        CreateMap<TestTableCreateDTO, TestTable>();
    }
}
