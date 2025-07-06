using TechFoodClean.Common.Entities;

namespace TechFoodClean.Application.Interfaces.DataSource
{
    public interface IUserDataSource
    {
        Task<Guid> CreateAsync(UserDTO user);

        Task<UserDTO?> GetByUsernameOrEmailAsync(string username);
    }
}
