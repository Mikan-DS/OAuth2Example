using Microsoft.EntityFrameworkCore;
using OAuth2CoreLib;
using OAuth2CoreLib.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("SqlLite");

var migrationsAssembly = typeof(Program).Assembly.GetName().Name;
builder.Services.AddDbContext<OAuthDbContext>(op => op.UseSqlite(connectionString, b => b.MigrationsAssembly(migrationsAssembly)));

builder.Services.AddScoped<IOAuth2Service, OAuth2Service>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();
