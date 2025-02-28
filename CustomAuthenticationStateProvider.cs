using System.Security.Claims;
using iTFORMS.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public CustomAuthenticationStateProvider(
        SignInManager<User> signInManager,
        UserManager<User> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            // Get the current user principal from the SignInManager context
            var principal = _signInManager.Context?.User;

            // If no authenticated user, return an anonymous state
            if (principal?.Identity?.IsAuthenticated != true)
            {
                return CreateAnonymousState();
            }

            // Retrieve the user from the database
            var user = await _userManager.GetUserAsync(principal);
            if (user == null)
            {
                return CreateAnonymousState();
            }

            // Create claims for the authenticated user
            var claims = await CreateUserClaimsAsync(user);

            // Create a new ClaimsIdentity and ClaimsPrincipal
            var identity = new ClaimsIdentity(claims, "CustomAuth");
            var userPrincipal = new ClaimsPrincipal(identity);

            return new AuthenticationState(userPrincipal);
        }
        catch (Exception ex)
        {
            // Log the error (you can use ILogger here if needed)
            Console.Error.WriteLine($"Error in GetAuthenticationStateAsync: {ex.Message}");
            return CreateAnonymousState();
        }
    }

    public async Task NotifyAuthenticationStateChangedAsync()
    {
        var authState = await GetAuthenticationStateAsync();
        NotifyAuthenticationStateChanged(Task.FromResult(authState));
    }

    private static AuthenticationState CreateAnonymousState()
    {
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    private async Task<List<Claim>> CreateUserClaimsAsync(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
            new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        // Add user roles as claims
        var roles = await _userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        return claims;
    }
}