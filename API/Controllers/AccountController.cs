using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(DataContext context, ITokenService tokenService) : BaseApiController
{
    // It does not make sense to put tags like [Authorize] or [AllowAnonymous] in the Register or Login, cause the user is not gonna be authenticated at this moment
    // About public async Task<ActionResult<UserDto>> Register(string username, string password)
    // Accordingly to the controller, the use of the parameters string in this case indicates that username and password will need to be passed in the query string of the request, not in the body or headers.
    // If we use an object instead of primitive types, the username and password will need to be passed in the body of the request so it can work.
    // We can use [FromBody] just before the parameter name in the function too. But the resolution used below is more conventional in C#, so that's why the parameter function is a DTO.

    // [HttpPost("register")] // path: api/account/register
    // public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto){

    //     if (await UserExists(registerDto.Username)) return BadRequest("This username already exists");

    //     // Using hmac to hash the password
    //     using var hmac = new HMACSHA512();

    //     var user = new AppUser {
    //         UserName = registerDto.Username,
    //         PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
    //         PasswordSalt = hmac.Key
    //     };

    //     // Adding an user using the dbcontext
    //     context.Users.Add(user);
        
    //     // Awaiting to save successfully
    //     await context.SaveChangesAsync();

    //     return new UserDto {
    //         Username = user.UserName,
    //         Token = tokenService.CreateToken(user)
    //     };
    // }

    [HttpPost("login")] // path: api/account/login
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto){
        // First thing when logging is verifying if the user exists in the DB
        var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName.ToLower());

        if (user is null) return Unauthorized("Invalid username");

        // To compare if the password inserted in the login corresponds to the hashed one in the DB, we have to be able to access two variables: the user.PasswordSalt (given by the user object) and the loginDto.Password.
        // With these two, we can initialize the hmac with the PasswordSalt as the key. Then, calling the ComputeHash method with the loginDto.Password given by the user as a parameter, we recover the PasswordHash and compare to the one that is already in the user object.
        using var hmac = new HMACSHA512(user.PasswordSalt);

        // If the loginDto.Password given is incorrect, the computedHash is gonna be different than the user.PasswordHash
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        // The hashes are array of bytes, so it must be compared in a loop. If any byte of the computedHash is different than the user.PasswordHash, it means that the loginDto.Password informed is wrong.
        for (int i = 0; i < computedHash.Length; i++){
            if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
        }
    
        return new UserDto{
            Username = user.UserName,
            Token = tokenService.CreateToken(user)
        };
    }

    // Equals does not work with entity in this case
    private async Task<bool> UserExists(string username){
        return await context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
    }
}
