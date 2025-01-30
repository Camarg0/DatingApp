using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions;

public static class IdentityServiceExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
    {
        // This is the service that tells asp.net to use JWT Bearer as the default method of auth. Basically means that the client must send a JWT in the Authorization header to gain access to HTTP requests.
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                // Retrieves the token from the appsettings.json
                var tokenKey = config["TokenKey"] ?? throw new Exception("TokenKey not found");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // Ensures the token is properly signed with the key
                    ValidateIssuerSigningKey = true,
                    // Defines the secret key used to verify the JWT
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
                    // The app will not check if the token was issued by a specific server. This is not necessary for my application because it is really low profile
                    ValidateIssuer = false,
                    // The app will not check if the token was meant for a specific audience.
                    ValidateAudience = false
                };
            });
            
        return services;
    }
}
