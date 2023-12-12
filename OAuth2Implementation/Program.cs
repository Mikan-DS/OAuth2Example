using Microsoft.EntityFrameworkCore;
using OAuth2CoreLib;
using OAuth2CoreLib.Services;
// using OAuth2Implementation.Models;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("SqlLite");

var migrationsAssembly = typeof(Program).Assembly.GetName().Name;
//Console.WriteLine(connectionString);
builder.Services.AddDbContext<OAuthDbContext>(op => op.UseSqlite(connectionString, b => b.MigrationsAssembly(migrationsAssembly)));
// builder.Services.AddDbContext<AuthenticationDbContext>(op => op.UseSqlite(connectionString, b => b.MigrationsAssembly(migrationsAssembly)));

builder.Services.AddScoped<IOAuth2Service, OAuth2Service>();

builder.Services.AddControllers();

//builder.Services.AddOAuth2Controllers();

var app = builder.Build();


//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


app.Run();
