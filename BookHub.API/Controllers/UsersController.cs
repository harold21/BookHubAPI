using Microsoft.AspNetCore.Mvc;
using BookHub.Core.Entities;
using BookHub.Core.Interfaces;
using BookHub.API.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace BookHub.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    public UsersController(IUserService userService, IAuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    // GET: /users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        var users = await _userService.GetAllUsersAsync();

        var userDtos = users.Select(user => new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Username = user.Username,
            Role = user.Role
        });

        return Ok(userDtos);
    }

    // GET: /users/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUserById(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        var userRespnse = new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Username = user.Username,
            Role = user.Role
        };

        return Ok(userRespnse);
    }

    // POST: /users
    [HttpPost]
    public async Task<ActionResult<User>> CreateUser([FromBody] CreateUserDto userDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdUser = await _userService.CreateUserAsync(new User {Name = userDto.Name, Username = userDto.Username, PasswordHash = userDto.Password});

        var userRespnse = new UserResponseDto
        {
            Id = createdUser.Id,
            Name = createdUser.Name,
            Username = createdUser.Username,
            Role = createdUser.Role
        };

        return CreatedAtAction(nameof(GetUserById), new { id = userRespnse.Id }, userRespnse);
    }

    // PUT: /users/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
    {
        if (id != user.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _userService.UpdateUserAsync(user);

        return NoContent();
    }

    // DELETE: /users/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        
        if (user == null)
        {
            return NotFound();
        }

        await _userService.DeleteUserAsync(id);

        return NoContent();
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = await _authService.AuthenticateUserAsync(loginDto.Username, loginDto.Password);
        
        if (user == null)
        {
            return Unauthorized();
        }

        var tokenString = _authService.GenerateJwtToken(user);

        Response.Headers.Append("Authorization", $"Bearer {tokenString}");

        var userResponse = new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Username = user.Username,
            Role = user.Role
        };

        return Ok(new { Token = tokenString, User = userResponse });
    }
}