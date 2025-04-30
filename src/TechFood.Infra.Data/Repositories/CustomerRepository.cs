using System;
using System.Threading.Tasks;
using TechFood.Domain.Entities;
using TechFood.Domain.Repositories;
using TechFood.Infra.Data.Contexts;

namespace TechFood.Infra.Data.Repositories;

internal class CustomerRepository(TechFoodContext dbContext) : ICustomerRepository
{
    private readonly TechFoodContext _dbContext = dbContext;
    public async Task<Guid> CreateAsync(Customer customer)
    {
        var entry = await _dbContext.AddAsync(customer);

        await entry.Context.SaveChangesAsync();

        return entry.Entity.Id;
    }
}
