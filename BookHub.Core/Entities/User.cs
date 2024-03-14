namespace BookHub.Core.Entities;

public class User
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Username { get; set; }
    public required string PasswordHash { get; set; }
    public string Role { get; set; } = "Common";
}