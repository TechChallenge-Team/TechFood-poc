using TechFoodClean.Common.DTO;
using TechFoodClean.Common.DTO.ValueObjects;

namespace TechFoodClean.Common.Entities;

public class UserDTO : EntityDTO
{

    public NameDTO Name { get; set; } = null!;

    public string Username { get; set; } = null!;

    public EmailDTO? Email { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string Role { get; set; } = null!;

}
