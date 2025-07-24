using EdTech.Quiz.Application.DTOs.Request;
using EdTech.Quiz.Application.DTOs.Response;

namespace EdTech.Quiz.Application.Interface.Services;

public interface IAuthService
{
    public Task<ResponseDTO> SignUp(RegisterDTO dto);

    public Task<ResponseDTO> SignIn(LoginDTO dto);

}
