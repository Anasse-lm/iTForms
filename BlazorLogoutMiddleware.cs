using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using iTFORMS.Models; // Ensure correct namespace for your User model

public class BlazorLogoutMiddleware
{
    private readonly RequestDelegate _next;

    public BlazorLogoutMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IServiceScopeFactory scopeFactory)
    {
        if (context.Request.Path == "/logout")
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var signInManager = scope.ServiceProvider.GetRequiredService<SignInManager<User>>();

                await signInManager.SignOutAsync();
                
                context.Response.Redirect("/login");
                return;
            }
        }

        await _next(context);
    }
}
