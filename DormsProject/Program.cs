using DormsProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var optionsBuilder = new DbContextOptionsBuilder<DormsContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DormsContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("DormsContext")));

optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
