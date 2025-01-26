using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    //Entity Framework is conventional based: Users is gonna be the name of the table in the database
    public DbSet<AppUser> Users { get; set; }

    //Also if I have an integer field, Entity Framework recognizes it as auto increment field to save in the database (always incrementing one)
}
