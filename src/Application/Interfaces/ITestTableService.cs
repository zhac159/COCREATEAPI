using Application.DTOs;

namespace Application.Interfaces
{
    public interface ITestTableService
    {
        Task<TestTableDTO> GetTestTableByIdAsync(int id);
        Task<TestTableDTO> CreateAsync(TestTableCreateDTO testTableCreateDTO);
    }
}