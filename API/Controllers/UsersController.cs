using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API;

[ApiController]
[Route("api/[controller]")] // /api/users ([controller] in this case indicates the name of that comes before the Controller syntax)
public class UsersController(DataContext context) : ControllerBase
{
    // When specifying the type in dotnet, we use the angled brackets <>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var users = await context.Users.ToListAsync();

        return users;

        // We can return the HttpResponse because of the ActionResult object, like below
        //return Ok(users);
    }

    // I can't create another Get for the same route, unless I specify more the get route like this
    [HttpGet("{id:int}")]  // api/users/3
    public async Task<ActionResult<AppUser>> GetUsers(int id)
    {
        var user = await context.Users.FindAsync(id);

        if (user == null) return NotFound(user);

        return user;
    }
}

/* General comments
    In this particular project, we're not gonna see any difference between async and synchronous code, because of the scale of the project: just a test app, so no or few users. 
*/