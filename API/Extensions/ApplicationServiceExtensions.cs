using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllers();
        services.AddDbContext<DataContext>(option =>
        {
            option.UseSqlite(config.GetConnectionString("DefaultConnection"));
        });
        services.AddCors();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        /* We need to tell dotnet how long a service is gonna live for, and we have 3 options:
         * Singleton creates an instance of the service in the first request and maintains it for the subsequent requests.
            - services.AddSingleton<ITokenService, TokenService>();
         * Transient are created each time a "new" is made, independent of the requests, and works better for stateless services.
            - services.AddTransients<ITokenService, TokenService>();
         * And the scoped as below, which in this case is the best:
        */
        // The scoped creates an instance of the service every request. So basically each httpRequest this one is called. When the request is received by the API Controller, when using dependency injection, the token service will be injected in the controller, so the service will be created. The token will be created, returned and then the service will be disposed when the request is done.
        // Specifying the ITokenService resolves it via ITokenService which decouples code, allowing switching implementations with way better testability too. Passing just the TokenService is possible but it makes the service tied to an specific implementation.
        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped<IUserRepository, UserRepository>();
        
        // Is gonna look inside the assembly and register all automapper profiles that I'm creating
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}
