using API.Controllers;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

// [Authorize] in this case would apply the attribute to all the methods here, unless a specific method has [AllowAnonymous], that would overwrite it
// [Route("api/[controller]")] /api/users ([controller] in this case indicates the name of that comes before the Controller syntax)
public class UsersController(DataContext context) : BaseApiController
{
    // When specifying the type in dotnet, we use the angled brackets <>
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var users = await context.Users.ToListAsync();

        return users;

        // We can return the HttpResponse because of the ActionResult object, like below
        //return Ok(users);
    }

    // I can't create another Get for the same route, unless I specify more the get route like this
    [Authorize] // Authorize means that it needs authentication (in this case from a token)
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