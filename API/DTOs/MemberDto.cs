namespace API.DTOs;

public class MemberDto
{
    public int Id { get; set; } 

    public string? UserName { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public string? KnownAs { get; set; }

    public DateTime Created { get; set; }

    public DateTime LastActive { get; set; }

    public string? Gender { get; set; }

    public string? Introduction { get; set; }

    public string? Interests { get; set; }

    public string? LookingFor { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    // Automapper detects the GetAge() and it is gonna populate the age with the result of the method in AppUser
    public int Age { get; set; }

    // Main photo url
    public string? PhotoUrl { get; set; }

    public List<PhotoDto>? Photos { get; set; }
}
