namespace Dsw2025Tpi.Application.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(string userId, string userName);
    }
}