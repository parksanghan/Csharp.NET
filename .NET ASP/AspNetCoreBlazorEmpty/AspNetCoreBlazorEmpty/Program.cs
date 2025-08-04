using Microsoft.EntityFrameworkCore;
using AspNetCoreBlazorEmpty.Models;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BlazorTDBContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:ProductConnection"]); // db연결
    opts.EnableSensitiveDataLogging(true);
});
builder.Services.AddControllersWithViews(); 
builder.Services.AddRazorPages();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseStaticFiles();
app.MapControllers();
app.MapControllerRoute("controllers", "controllers/{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
// 빈프로젝트 