using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using pract18.Models;
using pract18.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddDbContext<PostsContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("local"))
);
builder.Services.AddScoped<IPasswordService, Sha256PasswordService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPostsService, PostsService>();

builder
    .Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt =>
    {
        opt.LoginPath = "/Login";
        opt.Cookie.Name = "NotesUser";
        opt.ExpireTimeSpan = TimeSpan.FromHours(3);
    });

var app = builder.Build();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapGet("/Logout", async context => {
    await context.SignOutAsync();
    context.Response.Redirect("/");
});
app.UseStaticFiles();

app.MapRazorPages();

app.Run();
