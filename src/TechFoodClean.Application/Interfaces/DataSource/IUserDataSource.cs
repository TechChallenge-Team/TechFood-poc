using TechFoodClean.Common.DTO;

namespace TechFoodClean.Application.Interfaces.DataSource
{
    public interface IUserDataSource
    {
        Task<Guid> CreateAsync(UserDTO user);

        Task<UserDTO?> GetByUsernameOrEmailAsync(string username);
    }
}
