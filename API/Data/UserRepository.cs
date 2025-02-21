using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class UserRepository(DataContext context, IMapper mapper) : IUserRepository
{
    public async Task<IEnumerable<AppUser>> GetUsersAsync(){
        return await context.Users
            .Include(x => x.Photos)
            .ToListAsync();
    }

    public async Task<AppUser?> GetUserByIdAsync(int id){
        return await context.Users.FindAsync(id);
    }

    public async Task<AppUser?> GetUserByUsernameAsync(string username){
        // The username is unique for each user
        return await context.Users
            .Include(x => x.Photos)
            .SingleOrDefaultAsync(x => x.UserName == username);
    }

    public async Task<bool> SaveAllAsync(){
        // The SaveChanges returns an int, corresponding the number of changes that were made in the database. If 0, did not execute anything with success
        return await context.SaveChangesAsync() > 0;
    }

    // Entity framework already trackes the entities automatically anyway, so we don't have to make a context.Update for example. But, this code explicity tells Entity Framework that the entity has been modified. There may not be any ocasion in the code that we’re using it
    public void Update(AppUser user)
    {
        context.Entry(user).State = EntityState.Modified;
    }

    // Good thing about automapping the MemberDto is that we get from the DB only the essentials for the MemberDto, not all the AppUser
    public async Task<IEnumerable<MemberDto?>> GetMembersAsync(){
        return await context.Users
            .ProjectTo<MemberDto>(mapper.ConfigurationProvider) // Here we specify the projectTo, which is a queryable method to map to the correct object
            .ToListAsync();
    }

    public async Task<MemberDto?> GetMemberByUsernameAsync(string username){
        return await context.Users
            .Where(x => x.UserName == username)
            .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(); // Only one member
    }

    public async Task<MemberDto?> GetMemberByIdAsync(int id){
        return await context.Users
            .Where(x => x.Id == id)
            .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(); // Only one member
    }
}
