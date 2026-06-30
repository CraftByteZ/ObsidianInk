using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ObsidianInk.Data;

var builder = WebApplication.CreateBuilder(args);

// Razor Pages
builder.Services.AddRazorPages();

// Database
builder.Services.AddDbContext<ObsidianInkContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ObsidianInkContext")));

// HttpClient for API
builder.Services.AddHttpClient("api", client =>
{
    // 👇 This must point to your Web API project base URL, NOT the database port
    client.BaseAddress = new Uri("https://localhost:7144/");
});

// ✅ Enable Cookie Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login"; // ruta a redirigir si no está logueado
        options.LogoutPath = "/logout";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
    });

builder.Services.AddAuthorization();


var app = builder.Build();

// Middleware order is important!
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Authentication before Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
