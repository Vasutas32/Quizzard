using Quizzard.Components;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Quizzard.Data;
using Quizzard.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Quizzard.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddScoped<QuizService>();
builder.Services.AddScoped<IQrCodeService, QrCodeService>();

builder.Services.AddSingleton<Microsoft.AspNetCore.Identity.IPasswordHasher<Quizzard.Models.UserAccount>, Microsoft.AspNetCore.Identity.PasswordHasher<Quizzard.Models.UserAccount>>(); // <--- NEW

var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");
builder.Services.AddDbContext<QuizDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "QuizzardAuthCookie";
        options.LoginPath = "/login";
        options.Cookie.MaxAge = TimeSpan.FromMinutes(30);
        options.AccessDeniedPath = "/access-denied";
    });
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapGet("/LoginProcessor", async (string username, string password, HttpContext httpContext, QuizDbContext dbContext,
    Microsoft.AspNetCore.Identity.IPasswordHasher<Quizzard.Models.UserAccount> passwordHasher) => // <--- INJECTED) =>
{
    var userAccount = dbContext.UserAccounts.FirstOrDefault(u => u.Username == username);

    if (userAccount is null)
    {
        // Failed login, redirect back to the Blazor component with an error parameter
        return Results.Redirect("/login?error=Invalid credentials.");
    }
    var result = passwordHasher.VerifyHashedPassword(
        userAccount,
        userAccount.Password, // The stored hash
        password);

    if (result == Microsoft.AspNetCore.Identity.PasswordVerificationResult.Failed) // <--- IPasswordHasher method
    {
        return Results.Redirect("/login?error=Invalid credentials.");
    }
    // SUCCESSFUL LOGIN: HttpContext is available here!
    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Role, userAccount.Role),
        new Claim(ClaimTypes.Name, username),
        new Claim(ClaimTypes.NameIdentifier, userAccount.Id.ToString())
    };

    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
    var principal = new ClaimsPrincipal(identity);

    await httpContext.SignInAsync(principal);
    return Results.Redirect("/");
});

app.MapGet("/RegisterUser", async (
    HttpContext httpContext,
    string username,
    string password,
    QuizDbContext dbContext,
    Microsoft.AspNetCore.Identity.IPasswordHasher<Quizzard.Models.UserAccount> passwordHasher) => // <--- INJECTED) =>
{
    // 1. Check if user already exists
    var existingUser = dbContext.UserAccounts.FirstOrDefault(u => u.Username == username);
    if (existingUser != null)
    {
        return Results.Redirect("/login?error=Username+already+taken.");
    }

    // 2. Create the new user
    var newUser = new UserAccount // Assuming UserAccount model is available
    {
        Username = username,
        Role = "User"
    };

    var hashedPassword = passwordHasher.HashPassword(newUser, password);
    newUser.Password = hashedPassword;
    dbContext.UserAccounts.Add(newUser);
    await dbContext.SaveChangesAsync();

    // 3. Automatically log the new user in (reuse sign-in logic from above)
    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Role, newUser.Role),
        new Claim(ClaimTypes.Name, username)
    };

    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
    var principal = new ClaimsPrincipal(identity);

    await httpContext.SignInAsync(principal);

    // 4. Redirect to the home page
    return Results.Redirect("/");
});

app.MapGet("/logout-handler", async (HttpContext httpContext) =>
{
    // A Blazor kivezetõ pontja
    await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return Results.Redirect("/");
});

app.Run();
