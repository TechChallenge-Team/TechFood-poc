using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechFood.Domain.Entities;
using TechFood.Domain.Enums;
using TechFood.Domain.Repositories;
using TechFood.Infra.Data.Contexts;

namespace TechFood.Infra.Data.Repositories
{
    internal class CustomerRepository(TechFoodContext dbContext) : ICustomerRepository
    {
        private readonly TechFoodContext _dbContext = dbContext;

        public async Task<Guid> CreateAsync(Customer customer)
        {
            var entry = await _dbContext.AddAsync(customer);

            return entry.Entity.Id;
        }

        public async Task<Customer?> GetByDocument(DocumentType type, string value)
        {
            return await _dbContext.Customers
                .FirstOrDefaultAsync(c => c.Document.Type == type && c.Document.Value == value);
        }

        public Task<Customer?> GetByDocumentAsync(DocumentType documentType, string documentValue)
        {
            return _dbContext.Customers
                .FirstOrDefaultAsync(c => c.Document.Type == documentType && c.Document.Value == documentValue);
        }
    }
}
