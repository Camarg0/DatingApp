using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class Seed
{
    public static async Task SeedUsers(DataContext dbContext){
        // If there are users in the DB, we do not populate it
        if (await dbContext.Users.AnyAsync()) return;
        
        // Reading all the data in the Json file
        var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");

        // We can't work with a string, so we have to deserialize the json into an object
        
        var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};

        // In this moment, as the UserSeedData does not contain the PasswordHash / PasswordSalt, these fields cannot be required otheerwise it is gonna return an exception
        var users = JsonSerializer.Deserialize<List<AppUser>>(userData, options);

        if (users == null) return;

        // Populating the dbContext user so it can save the changes in the database after
        foreach (var user in users)
        {
            using var hmac = new HMACSHA512();
            
            user.UserName = user.UserName.ToLower();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
            user.PasswordSalt = hmac.Key;

            dbContext.Users.Add(user);
        }

        await dbContext.SaveChangesAsync();
    }
}
