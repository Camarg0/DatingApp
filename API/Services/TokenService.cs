using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class TokenService(IConfiguration config) : ITokenService
{
    // The logic here is to receive the tokenKey by the config parameter (which is set in the appsettings), specify the token descriptor, create a new JWT with this descriptor and return this JWT.
    public string CreateToken(AppUser user)
    {
        var tokenKey = config["TokenKey"] ?? throw new Exception("Cannot access TokenKey from appsettings.json");

        if (tokenKey.Length < 64) throw new Exception("Token provided must have at least 64 chars");

        // Generating a key from the tokenKey. Symmetric means that is not like a private and public key. Here it is the same key for encrypt and decrypt
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

        // Claims are informations that are associated with the user authenticated in the system
        var claims = new List<Claim>
        {
          new (ClaimTypes.NameIdentifier, user.UserName)
        };

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        // Initializing the token descriptor
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
        
        // The returned token must look like this: header.payload.signature
    }
}
