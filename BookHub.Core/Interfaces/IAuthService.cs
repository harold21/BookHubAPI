using BookHub.Core.Entities;

namespace BookHub.Core.Interfaces;

public interface IAuthService
{
    Task<User?> AuthenticateUserAsync(string username, string password);
    string GenerateJwtToken(User user);
    string HashPassword(string password);
}