using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;

// This annotation is important so Entity Framework can create the table as Photos, and not Photo as the class name 
[Table("Photos")]
public class Photo
{
    public int Id { get; set; }

    public required string Url { get; set; }

    public bool IsMain { get; set; }

    public string? PublicId { get; set; }

    // Conventionally, the Entity Framework does not create a foreign key of 'Photos' in the 'User' table, so we have to specify in the navigation properties this dependency, so we cannot exclude a user without exclude its photos
    // Navigation Properties
    public int AppUserId { get; set; }
    // In the migrations PhotoEntityCreated, we can see  the nullable false in the AppUserId in the photos table and the onDelete: ReferentialAction.Cascade. That means if we delete any appUser, we delete the photos related to them.
    public AppUser AppUser { get; set; } = null!; 
}
