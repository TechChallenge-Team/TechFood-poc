using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechFood.Domain.Entities;
using TechFood.Domain.Repositories;
using TechFood.Infra.Data.Contexts;

namespace TechFood.Infra.Data.Repositories;

public class PaymentRepository(TechFoodContext dbContext) : IPaymentRepository
{
    private readonly DbSet<Payment> _payments = dbContext.Payments;

    public async Task<Guid> AddAsync(Payment payment)
    {
        var entry = await _payments.AddAsync(payment);

        return entry.Entity.Id;
    }

    public Task<Payment?> GetByIdAsync(Guid id)
    {
        return _payments
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
