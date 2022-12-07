using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Animal_Protection.Areas.Identity.Data;
using Animal_Protection.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using Protection_Animal.Utility;
using Animal_Protection.Controllers;
using Animal_Protection.Initializer;
using ProjectAnimal.Model.Repository;
using StudentManager.Model.Repositories;
using Protection_Animal.Model.Entities;
using Protection_Animal.Infrastructure.Managers.Interfaces;
using Protection_Animal.Infrastructure.Managers.Implemetations;
using Microsoft.Extensions.Logging;
using StudentManager.WebApp.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("IdentityContextConnection") ?? throw new InvalidOperationException("Connection string 'IdentityContextConnection' not found.");

builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<AnimalProtectionUser, IdentityRole>()
            .AddEntityFrameworkStores<IdentityContext>()
            .AddDefaultTokenProviders()
            .AddDefaultUI();

//AddDbLogger(builder.Logging, options =>
//{
//    builder.Configuration.GetSection("Logging").GetSection("Database").GetSection("Options").Bind(options);
//});

builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromHours(10);
    option.Cookie.HttpOnly = true;
    option.Cookie.IsEssential = true;
});

builder.Services.AddTransient<ClientsController>();

builder.Services.AddTransient<IRepository<Application, int>, DbRepository<Application, int>>();
builder.Services.AddTransient<IApplicationManager, ApplicationsManagers>();

builder.Services.AddTransient<IRepository<ApplicationCategory, int>, DbRepository<ApplicationCategory, int>>();
builder.Services.AddTransient<ICategoryManager, CategoryManager>();

builder.Services.AddTransient<IRepository<Animal, int>, DbRepository<Animal, int>>();
builder.Services.AddTransient<IAnimalManager, AnimalManager>();

builder.Services.AddTransient<IRepository<Client, string>, DbRepository<Client, string>>();
builder.Services.AddTransient<IClientManager, ClientManager>();

builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer("ConnectionString"));

builder.Services.AddAuthentication().AddFacebook(options =>
{
    options.AppId = "1214617792823832";
    options.AppSecret = "3d4935483c23d5c5ae5e0c089ed857b3";
});

builder.Services.AddAuthentication().AddGoogle(options =>
{
    options.ClientId = "570673526187-46pdarq9kp89af9j8bsvebjvm7sqeju3.apps.googleusercontent.com";
    options.ClientSecret = "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";
});

builder.Services.AddScoped<IDbInitializer, DbInitializer>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseRouting();

app.UseSession();

SeedDatabase();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();

void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}

//static ILoggingBuilder AddDbLogger(ILoggingBuilder builder, Action<DbLoggerOptions> configure)
//{
//    builder.Services.AddSingleton<ILoggerProvider, DbLoggerProvider>();
//    builder.Services.Configure(configure);
//    return builder;
//}
