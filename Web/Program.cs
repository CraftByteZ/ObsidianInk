using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ObsidianInk.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// Database
builder.Services.AddDbContext<ObsidianInkContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ObsidianInkContext")));

// HttpClient for API
builder.Services.AddHttpClient("api", client =>
{
    client.BaseAddress = new Uri("https://localhost:7144/"); // Example API base URL
});

// Simple cookie auth for demo
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.AccessDeniedPath = "/Login";
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
