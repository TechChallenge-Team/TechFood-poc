using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechFood.Domain.Entities;
using TechFood.Domain.Repositories;
using TechFood.Infra.Data.Contexts;

namespace TechFood.Infra.Data.Repositories
{
    internal class OrderRepository(TechFoodContext dbContext) : IOrderRepository
    {
        private readonly TechFoodContext _dbContext = dbContext;

        public async Task<Guid> CreateAsync(Order order)
        {
            var entry = await _dbContext.AddAsync(order);

            await entry.Context.SaveChangesAsync();

            return entry.Entity.Id;
        }

        public async Task<Order> FindByIdAsync(Guid id)
        {
            var order = await _dbContext
                .Orders
                .Include(o => o.Payment)
                .Include(o => o.Items)
                .FirstAsync(o => o.Id == id);

            return order;
        }

        public async Task UpdateAsync(Order order)
        {
            _dbContext.Orders.Update(order);

            await _dbContext.SaveChangesAsync();
        }
    }
}
