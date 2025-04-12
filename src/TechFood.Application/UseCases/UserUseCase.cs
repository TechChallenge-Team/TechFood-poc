using AutoMapper;
using System.Threading.Tasks;
using TechFood.Application.Models;
using TechFood.Application.UseCases.Interfaces;
using TechFood.Domain.Entities;
using TechFood.Domain.Repositories;

namespace TechFood.Application.UseCases
{
    public class UserUseCase(
        IMapper mapper,
        IUserRepository userRepository) : IUserUseCase
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            return _mapper.Map<User, UserDto>(user);
        }
    }
}
