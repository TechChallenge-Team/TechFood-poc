using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TechFood.Domain.Entities;
using TechFood.Domain.Repositories;

namespace TechFood.Application.UseCases.Authentication.Commands;

public class SignInCommand : IRequest<SignInCommand.Result>
{
    [Required]
    public string Username { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;

    public class Handler(
        IUserRepository repo,
        IConfiguration configuration)
            : IRequestHandler<SignInCommand, Result>
    {
        private static readonly TimeSpan _tokenExpiration = TimeSpan.FromHours(1);

        public async Task<Result> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var user = await repo.GetByUsernameOrEmailAsync(request.Username);
            if (user == null)
            {
                throw new Common.Exceptions.ApplicationException(Common.Resources.Exceptions.Auth_InvalidUseOrPassword);
            }

            var validation = new PasswordHasher<User>();
            var isValid = validation.VerifyHashedPassword(user, user.PasswordHash, request.Password);

            if (isValid == PasswordVerificationResult.Failed)
            {
                throw new Common.Exceptions.ApplicationException(Common.Resources.Exceptions.Auth_InvalidUseOrPassword);
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: new[]
                {
                    new Claim(ClaimTypes.Name, request.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                },
                expires: DateTime.UtcNow.Add(_tokenExpiration),
                audience: configuration["Jwt:Audience"],
                issuer: configuration["Jwt:Issuer"],
                signingCredentials: creds);

            return new()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresIn = (int)_tokenExpiration.TotalSeconds,
                User = new()
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email?.Address,
                    Name = user.Name.FullName,
                    Role = user.Role
                }
            };
        }
    }

    public class Result
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
}
