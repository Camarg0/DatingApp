using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

// Authorize means that it needs authentication (in this case from a token). In this case applies the attribute to all the methods here, unless a specific method has [AllowAnonymous], that would overwrite it
// [Route("api/[controller]")] /api/users ([controller] in this case indicates the name of that comes before the Controller syntax)
[Authorize]
public class UsersController(IUserRepository userRepository) : BaseApiController
{
    // When specifying the type in dotnet, we use the angled brackets <>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        var users = await userRepository.GetMembersAsync();

        // We can return the HttpResponse because of the ActionResult object, like below
        return Ok(users);
    }

    // I can't create another Get for the same route, unless I specify more the get route like this
    [HttpGet("{id:int}")]  // api/users/3
    public async Task<ActionResult<MemberDto>> GetUserById(int id)
    {
        var user = await userRepository.GetMemberByIdAsync(id);

        if (user == null) return NotFound(user);

        return Ok(user);
    }

    [HttpGet("{username}")]  // api/users/lisa
    public async Task<ActionResult<MemberDto>> GetUserByUsername(string username)
    {
        var user = await userRepository.GetMemberByUsernameAsync(username);

        if (user == null) return NotFound(user);

        return Ok(user);
    }
}

/* General comments
    In this particular project, we're not gonna see any difference between async and synchronous code, because of the scale of the project: just a test app, so no or few users. 
*/