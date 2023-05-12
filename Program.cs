using ChapterOne.DataAccessLayer;
using ChapterOne.Interfaces;
using ChapterOne.Services;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("default"));
});
builder.Services.AddScoped<ILayoutService, LayoutServices>();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
});
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseStaticFiles();
app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
          );
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

app.Run();
