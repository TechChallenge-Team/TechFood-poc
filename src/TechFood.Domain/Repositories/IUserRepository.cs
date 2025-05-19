using System;
using System.Threading.Tasks;
using TechFood.Domain.Entities;

namespace TechFood.Domain.Repositories;

public interface IUserRepository
{
    Task<Guid> CreateAsync(User user);

    Task<User?> GetByUsernameOrEmailAsync(string username);
}
