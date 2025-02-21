using API.DTOs;
using API.Entities;

namespace API.Interfaces;

public interface IUserRepository
{
    public void Update(AppUser user);

    // Conventionally we use Async in declaration for methods that returns a Task<> and will have to wait
    public Task<bool> SaveAllAsync();

    public Task<IEnumerable<AppUser>> GetUsersAsync();

    public Task<AppUser?> GetUserByIdAsync(int id);

    // We want the ability to return null if no user is found (optional). Making them optional, the code that uses these methods are gonna give us warnings to implement a defensive code based on the fact that the result can be null
    public Task<AppUser?> GetUserByUsernameAsync(string username);

    public Task<IEnumerable<MemberDto?>> GetMembersAsync();

    public Task<MemberDto?> GetMemberByUsernameAsync(string username);

    public Task<MemberDto?> GetMemberByIdAsync(int id);
}
