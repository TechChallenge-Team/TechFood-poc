using Microsoft.EntityFrameworkCore;
using TechFood.Application.Interfaces.DataSource;
using TechFood.Common.Entities;
using TechFood.Infrastructure.Data.Contexts;

namespace TechFood.Infrastructure.Data.Repositories;

public class PaymentRepository(TechFoodContext dbContext) : IPaymentDataSource
{
    private readonly DbSet<PaymentDTO> _payments = dbContext.Payments;

    public async Task<Guid> AddAsync(PaymentDTO payment)
    {
        var entry = await _payments.AddAsync(payment);

        return entry.Entity.Id;
    }

    public Task<PaymentDTO?> GetByIdAsync(Guid id)
    {
        return _payments
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateAsync(PaymentDTO payment)
    {
        await Task.FromResult(_payments.Update(payment));
    }
}
