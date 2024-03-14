using System.ComponentModel.DataAnnotations;

namespace BookHub.API.DTOs;

public class LoginDto
{
    [Required]
    [EmailAddress]
    public string Username { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}