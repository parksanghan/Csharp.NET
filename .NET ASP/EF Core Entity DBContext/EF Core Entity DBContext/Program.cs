using EF_Core_Entity_DBContext.DbContexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FirstAppContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:ProductConnection"]);  
    opts.EnableSensitiveDataLogging(true);
});
// db context 세팅    
var app = builder.Build();
app.MapGet("/", () => "Hello World!");
app.Run();
