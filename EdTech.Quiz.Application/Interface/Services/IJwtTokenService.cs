namespace EdTech.Quiz.Application.Interface.Services;


public interface IJwtTokenService
{
    public string GenerateToken(int UserId, string Role);

}