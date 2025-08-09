using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AOWebApp.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<AOWebAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AOWebAppContext") ?? throw new InvalidOperationException("Connection string 'AOWebAppContext' not found.")));

builder.Services.AddDbContext<AmazonOrders2025Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AmazonOrders2025Context") ?? throw new InvalidOperationException("Connection string 'AmazonOrders2025Context' not found.")));

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
