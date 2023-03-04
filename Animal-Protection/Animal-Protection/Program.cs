using Animal_Protection.Areas.Identity.Data;
using Animal_Protection.Controllers;
using Animal_Protection.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using ProjectAnimal.Model.Repository;
using Protection_Animal.Infrastructure.Managers.Implemetations;
using Protection_Animal.Infrastructure.Managers.Interfaces;
using Protection_Animal.Model.Entities;
using Protection_Animal.Utility;
using StudentManager.Model.Repositories;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("IdentityContextConnection") ?? throw new InvalidOperationException("Connection string 'IdentityContextConnection' not found.");

builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<AnimalProtectionUser, IdentityRole>()
            .AddEntityFrameworkStores<IdentityContext>()
            .AddDefaultTokenProviders()
            .AddDefaultUI();



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
    options.ClientId = "570673526187-reviqt2ib30md0hurqag8p4dti2pmt23.apps.googleusercontent.com";
    options.ClientSecret = "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 ";
});

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



