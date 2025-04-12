using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechFood.Domain.Entities;
using TechFood.Domain.Repositories;
using TechFood.Infra.Data.Contexts;

namespace TechFood.Infra.Data.Repositories
{
    public class PaymentTypeRepository(TechFoodContext dbContext) : IPaymentTypeRepository
    {
        private readonly DbSet<PaymentType> _paymentTypes = dbContext.PaymentTypes;

        public async Task<IEnumerable<PaymentType>> GetAllAsync()
        {
            return await _paymentTypes.ToListAsync();
        }
    }
}
