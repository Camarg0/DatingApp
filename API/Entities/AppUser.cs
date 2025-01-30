using Microsoft.AspNetCore.Identity;

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
    public required byte[] PasswordHash { get; set; }

    public required byte[] PasswordSalt { get; set; }
}