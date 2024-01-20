using Domain.Entities;

namespace Domain.Interfaces;

public interface ITestTableRepository
{
    Task<TestTable?> GetByIdAsync(int id);
    Task<TestTable> CreateAsync(TestTable testTable);
}
