
using BookHub.Core.Entities;

namespace BookHub.Identity.Interfaces;

public interface IIdentityService
{
    Task<User?> AuthenticateUserAsync(string username, string password);
    string GenerateJwtToken(User user);
    string HashPassword(string password);
}