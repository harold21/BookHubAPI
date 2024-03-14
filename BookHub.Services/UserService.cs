using BookHub.Core.Entities;
using BookHub.Core.Interfaces;
using BookHub.Identity.Interfaces;

namespace BookHub.Services;

public class UserService : IUserService
{
    public readonly IUserRepository _userRepository;
    public readonly IIdentityService _identityService;

    public UserService(IUserRepository userRepository, IIdentityService identityService)
    {
        _userRepository = userRepository;
        _identityService = identityService;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task<User> CreateUserAsync(User user)
    {
        user.PasswordHash = _identityService.HashPassword(user.PasswordHash);
        
        await _userRepository.AddAsync(user);
        
        return user;
    }

    public async Task UpdateUserAsync(User user)
    {
        await _userRepository.UpdateAsync(user);
    }

    public async Task DeleteUserAsync(int id)
    {
        await _userRepository.DeleteAsync(id);
    }
}
