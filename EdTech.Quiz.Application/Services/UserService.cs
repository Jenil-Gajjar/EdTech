using EdTech.Quiz.Application.DTOs;
using EdTech.Quiz.Application.Interface.Repositories;
using EdTech.Quiz.Application.Interface.Services;
using EdTech.Quiz.Domain.Entities;

namespace EdTech.Quiz.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository) => _userRepository = userRepository;


    public async Task<int> CreateUserAsync(CreateUserDTO dto)
    {
        if(await _userRepository.DoesUserAlreadyExists(dto.Name)) throw new Exception("Name already exists");

        User user = new() { Name = dto.Name.Trim() };
        await _userRepository.AddUserAsync(user);
        await _userRepository.SaveChangesAsync();
        return user.Id;

    }

   

}
