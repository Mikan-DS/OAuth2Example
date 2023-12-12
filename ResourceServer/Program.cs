using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using ResourceServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true, // Включение проверки ключа подписи издателя
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1234567890123456789012345678901234567890123456789012345678901234567890")),
            ValidateAudience = false, // Отключение проверки аудитории
            ValidateIssuer = false, // Отключение проверки издателя
            ValidateLifetime = true // Включение проверки срока действия токена
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
    {
        policy.RequireAuthenticatedUser();
        //policy.RequireScope("test_cli.test");
        policy.RequireScope("cc.admin");
    });
});

builder.Services.AddSingleton<IAuthorizationHandler, ScopeHandler>();

builder.Services.AddControllers();


//builder.Services.AddControllersWithViews();


//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            //ValidateIssuer = false, // На данный момент мы ничего не проверяем
//            //ValidateAudience = false,
//            //ValidateLifetime = false,
//            //ValidateIssuerSigningKey = false,
//            //ValidIssuer = "адрес сервера авторизации",
//            ////ValidAudience = "",
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1234567890123456789012345678901234567890123456789012345678901234567890"))
//        };
//    });

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("AdminPolicy", policy =>
//    {
//        policy.RequireAuthenticatedUser();
//        //policy.RequireScope("test_cli.admin");
//    });
//});

//builder.Services.AddSingleton<IAuthorizationHandler, ScopeHandler>();

//builder.Services.AddAuthentication("Bearer")
//    .AddJwtBearer("Bearer", options =>
//    {
//        // Устанавливаем URL-адрес, используемый в качестве Authority для проверки токенов
//        options.Authority = Environment.GetEnvironmentVariable("ASPNETCORE_URLS"); // Используется текущий сервер
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateAudience = false,
//        };
//    });
//builder.Services.AddAuthorization(

//    options =>
//    {
//        options.AddPolicy("AdminPolicy",
//            policy =>
//            {
//                policy.RequireAuthenticatedUser();
//                //policy.RequireClaim("Scope", "Test");
//            }

//            );
//    });

//// Добавляем аутентификацию с помощью токенов JWT
//builder.Services.AddJwtTokenHandler();
//builder.Services.AddAuthentication("X509").AddScheme<JwtBearerOptions, JwtBearerWithUserInfoHandler>("X509", null);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();



//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
}); ;

app.Run();
