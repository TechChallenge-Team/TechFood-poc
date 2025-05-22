using System;

namespace TechFood.Application.Models.Auth;

public class SignInResult
{
    public string AccessToken { get; set; } = null!;

    public string RefreshToken { get; set; } = null!;

    public int ExpiresIn { get; set; }

    public SignInUser User { get; set; } = null!;

    public class SignInUser
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Email { get; set; }

        public string Username { get; set; } = null!;

        public string Role { get; set; } = null!;
    }
}
