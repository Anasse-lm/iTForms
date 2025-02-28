using System.Security.Claims;
using iTFORMS.IServices;
using Microsoft.AspNetCore.Components.Authorization;

public class UserService : IUserService
{
    private readonly AuthenticationStateProvider _authProvider;

    public UserService(AuthenticationStateProvider authProvider)
    {
        _authProvider = authProvider;
    }

    public async Task<Guid> GetCurrentUserId()
    {
        var authState = await _authProvider.GetAuthenticationStateAsync();
        var userId = authState.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Guid.Parse(userId);
    }
}