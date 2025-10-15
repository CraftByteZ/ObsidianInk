using Microsoft.EntityFrameworkCore;
using ObsidianInk.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add DbContext with PostgreSQL provider
builder.Services.AddDbContext<ObsidianInkContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ObsidianInkContext") ?? throw new InvalidOperationException("Connection string 'ObsidianInkContext' not found.")));

builder.Services.AddHttpClient("api", client =>
{
    client.BaseAddress = new Uri("https://localhost:5432/");
    //5432 is the default port for PostgreSQL, but typically APIs run on different ports like 5000 or 5001 for HTTPS.
    // 7144
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
