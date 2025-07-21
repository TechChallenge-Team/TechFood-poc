using TechFood.Common.Entities;

namespace TechFood.Application.Interfaces.DataSource
{
    public interface IUserDataSource
    {
        Task<Guid> CreateAsync(UserDTO user);

        Task<UserDTO?> GetByUsernameOrEmailAsync(string username);
    }
}
