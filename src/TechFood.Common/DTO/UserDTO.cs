using TechFood.Common.DTO;
using TechFood.Common.DTO.ValueObjects;

namespace TechFood.Common.Entities;

public class UserDTO : EntityDTO
{

    public NameDTO Name { get; set; } = null!;

    public string Username { get; set; } = null!;

    public EmailDTO? Email { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string Role { get; set; } = null!;

}
