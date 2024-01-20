using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class TestTableRepository : ITestTableRepository
    {
        private readonly CoCreateDbContext _context;

        public TestTableRepository(CoCreateDbContext context)
        {
            _context = context;
        }

        public async Task<TestTable> CreateAsync(TestTable testTable)
        {
            await _context.TestTables.AddAsync(testTable);
            await _context.SaveChangesAsync();

            return testTable;
        }

        public async Task<TestTable?> GetByIdAsync(int id)
        {
            return await _context.TestTables.FindAsync(id);
        }

         
        
    }
}