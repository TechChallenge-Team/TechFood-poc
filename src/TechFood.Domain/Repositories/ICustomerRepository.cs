using System.Threading.Tasks;
using System;
using TechFood.Domain.Entities;

namespace TechFood.Domain.Repositories;

public interface ICustomerRepository
{
    Task<Guid> CreateAsync(Customer customer);
}
