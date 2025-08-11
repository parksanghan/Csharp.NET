using EF_Core_Entity_DBContext.DbContexts;
using EF_Core_Entity_DBContext.Entities;
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
// migraion 테이블 테스트
using var scope= app.Services.CreateScope(); 
var db  = scope.ServiceProvider.GetRequiredService<FirstAppContext>();
await db.Database.MigrateAsync();
db.LogHistories.Add(new LogHistory(detail: "TEST"));
await db.SaveChangesAsync();
var conn = db.Database.GetDbConnection();
Console.WriteLine($"Connected DB: {conn.Database}, Server: {conn.DataSource}");
app.Run();
