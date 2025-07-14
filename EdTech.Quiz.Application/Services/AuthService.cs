using BCrypt.Net;
using EdTech.Quiz.Application.DTOs;
using EdTech.Quiz.Application.Interface.Repositories;
using EdTech.Quiz.Application.Interface.Services;
using EdTech.Quiz.Domain.Entities;
namespace EdTech.Quiz.Application.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;

    public AuthService(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<ResponseDTO> SignIn(LoginDTO dto)
    {
        User? user = await _authRepository.GetUserByUsername(dto.Username.Trim());

        if (user == null)
        {
            return new()
            {
                IsSuccess = false,
                Message = "Invalid Username or Password."
            };
        }
        bool verified = BCrypt.Net.BCrypt.Verify(dto.Password.Trim(), user.Password.Trim());

        if (verified)
            return new()
            {
                IsSuccess = true,
                Message = "Login Successfull."
            };

        else
            return new()
            {
                IsSuccess = false,
                Message = "Invalid Username or Password."
            };

    }


    public async Task<ResponseDTO> SignUp(RegisterDTO dto)
    {
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        User user = new()
        {
            UserName = dto.Username.Trim(),
            Email = dto.Email.Trim(),
            Password = passwordHash.Trim()
        };
        return await _authRepository.CreateUser(user);
    }
}
