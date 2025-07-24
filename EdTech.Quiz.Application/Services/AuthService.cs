using EdTech.Quiz.Application.DTOs.Request;
using EdTech.Quiz.Application.DTOs.Response;
using EdTech.Quiz.Application.Exceptions;
using EdTech.Quiz.Application.Interface.Repositories;
using EdTech.Quiz.Application.Interface.Services;
using EdTech.Quiz.Domain.Entities;
using EdTech.Quiz.Domain.Enums;
namespace EdTech.Quiz.Application.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;

    private readonly IJwtTokenService _jwtTokenService;
    public AuthService(IAuthRepository authRepository, IJwtTokenService jwtTokenService)
    {
        _authRepository = authRepository;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<ResponseDTO> SignIn(LoginDTO dto)
    {
        User? user = await _authRepository.GetUserByUsername(dto.Username.Trim()) ?? throw new AuthenticationFailedException();

        bool verified = BCrypt.Net.BCrypt.Verify(dto.Password.Trim(), user.Password.Trim());

        if (!verified) throw new AuthenticationFailedException();

        string jwtToken = _jwtTokenService.GenerateToken(user.Id, user.Role.RoleName);
        return new()
        {
            Data = jwtToken,
            IsSuccess = true,
            Message = "Login successfull."
        };

    }

    public async Task<ResponseDTO> SignUp(RegisterDTO dto)
    {
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        User user = new()
        {
            UserName = dto.Username.Trim(),
            Email = dto.Email.Trim().ToLower(),
            Password = passwordHash.Trim(),
            RoleId = (int)RoleEnum.User
        };
        return await _authRepository.CreateUser(user);
    }
}
