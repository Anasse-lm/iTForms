using iTFORMS.Components;
using Microsoft.EntityFrameworkCore;
using CloudinaryDotNet;
using dotenv.net;
using iTFORMS.IServices;
using iTFORMS.Services;
using iTFORMS.Data;
using iTFORMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add Razor Pages support for Identity UI
builder.Services.AddRazorPages();


// Configure DBContext with SQL Server
builder.Services.AddDbContext<DBContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Configure Identity with custom User and Role types
builder.Services.AddIdentity<User, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<DBContext>()
    .AddDefaultTokenProviders();

// Register custom authentication state provider
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login";
    options.AccessDeniedPath = "/access-denied"; 
});

// Register application services
builder.Services.AddScoped<ITemplateService, TemplateService>();
builder.Services.AddScoped<IUserService, UserService>();

// Configure HttpClient
builder.Services.AddHttpClient();

// Load environment variables
DotEnv.Load(options: new DotEnvOptions(probeForEnv: true, probeLevelsToSearch: 5));

// Validate and configure Cloudinary
var cloudinaryUrl = Environment.GetEnvironmentVariable("CLOUDINARY_URL") 
    ?? throw new InvalidOperationException("CLOUDINARY_URL is missing in .env!");

builder.Services.AddSingleton(new Cloudinary(cloudinaryUrl) { Api = { Secure = true } });

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();


app.UseAuthentication();
app.UseAuthorization();

// Configure endpoints
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Map Razor Pages for Identity
app.MapRazorPages();

app.Run();