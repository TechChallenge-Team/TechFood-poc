using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TechFood.Application.Models.Auth;
using TechFood.Application.UseCases.Interfaces;
using TechFood.Domain.Entities;
using TechFood.Domain.Repositories;

namespace TechFood.Application.UseCases;

internal class AuthUseCase(
    IConfiguration configuration,
    IUserRepository userRepository) : IAuthUseCase
{
    private static readonly TimeSpan _tokenExpiration = TimeSpan.FromHours(1);

    private readonly IConfiguration _configuration = configuration;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Models.Auth.SignInResult> SignInAsync(SignInRequest request)
    {
        var user = await _userRepository.GetByUsernameOrEmailAsync(request.Username);
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

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: new[]
            {
                new Claim(ClaimTypes.Name, request.Username),
                new Claim(ClaimTypes.Role, user.Role)
            },
            expires: DateTime.UtcNow.Add(_tokenExpiration),
            audience: _configuration["Jwt:Audience"],
            issuer: _configuration["Jwt:Issuer"],
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
