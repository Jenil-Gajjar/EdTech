using EdTech.Quiz.Application.DTOs;

namespace EdTech.Quiz.Application.Interface.Services;

public interface IAuthService
{
    public Task<ResponseDTO> SignUp(RegisterDTO dto);

    public Task<ResponseDTO> SignIn(LoginDTO dto);

}
