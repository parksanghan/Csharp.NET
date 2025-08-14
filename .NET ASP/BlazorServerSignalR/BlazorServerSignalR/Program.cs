using BlazorServerSignalR.Models;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddDbContext<BlazorTDBContext>(opts => {
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:NorthwindConnection"]);  
    opts.EnableSensitiveDataLogging(true);
});

var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

app.UseStaticFiles();
app.MapControllers();
app.MapControllerRoute("controllers", "controllers/{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.MapBlazorHub();