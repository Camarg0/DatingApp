namespace API.DTOs;

// These informations are gonna be returned to the user whenever a make a register or login request
public class UserDto
{
    public required string Username { get; set; }
    public required string Token { get; set; }
}
