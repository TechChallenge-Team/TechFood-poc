using Microsoft.EntityFrameworkCore;
using TechFoodClean.Application.Interfaces.DataSource;
using TechFoodClean.Common.DTO;
using TechFoodClean.Common.DTO.Enums;
using TechFoodClean.Infrastructure.Data.Contexts;

namespace TechFoodClean.Infrastructure.Data.Repositories
{
    internal class CustomerRepository(TechFoodContext dbContext) : ICustomerDataSource
    {
        private readonly TechFoodContext _dbContext = dbContext;

        public async Task<Guid> CreateAsync(CustomerDTO customer)
        {
            var entry = await _dbContext.AddAsync(customer);

            await entry.Context.SaveChangesAsync();

            return entry.Entity.Id;
        }

        public async Task<CustomerDTO?> GetByDocumentAsync(DocumentTypeDTO documentType, string documentValue)
        {
            return await _dbContext.Customers
                .FirstOrDefaultAsync(c => c.Document.Type == documentType && c.Document.Value == documentValue);
        }
    }
}
