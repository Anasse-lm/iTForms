using iTFORMS.Components;
using Microsoft.EntityFrameworkCore;
using CloudinaryDotNet;
using dotenv.net;
using iTFORMS.IServices;
using iTFORMS.Services;
using iTFORMS.Data;
using iTFORMS.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login";
    options.AccessDeniedPath = "/access-denied";
});

builder.Services.AddAuthorizationCore();

// Configure Database Context
builder.Services.AddDbContext<DBContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Identity Configuration
builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
    {
        options.Lockout.AllowedForNewUsers = false;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 5;

        options.SignIn.RequireConfirmedEmail = false; 
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddEntityFrameworkStores<DBContext>()
    .AddRoles<IdentityRole<Guid>>()
    .AddSignInManager()
    .AddDefaultTokenProviders();
    

// Register application services
builder.Services.AddScoped<ITemplateService, TemplateService>();
builder.Services.AddScoped<IUserService, UserService>();

// Configure HttpClient
builder.Services.AddHttpClient();

// Load environment variables
DotEnv.Load(options: new DotEnvOptions(probeForEnv: true, probeLevelsToSearch: 5));

// Cloudinary Configuration
var cloudinaryUrl = Environment.GetEnvironmentVariable("CLOUDINARY_URL") 
    ?? throw new InvalidOperationException("CLOUDINARY_URL is missing in .env!");

builder.Services.AddSingleton(new Cloudinary(cloudinaryUrl) { Api = { Secure = true } });

var app = builder.Build();

// Middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.UseMiddleware<BlazorLogoutMiddleware>();

// Order matters: Authentication -> Authorization -> Custom Middleware
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<BlazorCookieLoginMiddleware>();

// Configure Blazor components
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


app.Run();
