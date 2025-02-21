using API.Extensions;

namespace API.Entities;

//Each Entity represents a Table
public class AppUser
{
    //Each property represents a Column
    //Each time a user is added, the Id is increased. Each user has its unique id
    //The name 'Id' is a pattern used by the EntityFramework, and determines the PK to identify the register
    public int Id { get; set; } 

    //Required is a modifier added in dotnet, so we cannot create an AppUser without a UserName. It is used in strings and other reference types
    public required string UserName { get; set; }

    //For authentication, we're gonna make something really basic firstly, with the use of passwordhash and password salt
    public byte[] PasswordHash { get; set; } = [];

    public byte[] PasswordSalt { get; set; } = [];

    public DateOnly DateOfBirth { get; set; }

    public required string KnownAs { get; set; }

    public DateTime Created { get; set; } = DateTime.UtcNow;

    public DateTime LastActive { get; set; } = DateTime.UtcNow;

    public required string Gender { get; set; }

    public string? Introduction { get; set; }

    public string? Interests { get; set; }

    public string? LookingFor { get; set; }

    public required string City { get; set; }

    public required string Country { get; set; }

    // Here the Entity Framework creates the table automatically for Photos, because this property is interpreted as a navigation property. This means that EF recognizes the relationship between the table User and Photos
    public List<Photo> Photos { get; set; } = [];

    // The method must have the Get in the name for the AutoMapper understanding
    // public int GetAge(){
    //     return DateTimeExtensions.CalculateAge(DateOfBirth);
    // }
}