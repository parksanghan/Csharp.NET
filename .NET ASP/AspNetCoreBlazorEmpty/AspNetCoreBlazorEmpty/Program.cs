using Microsoft.EntityFrameworkCore;
using AspNetCoreBlazorEmpty.Models;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BlazorTDBContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:ProductConnection"]); // db����
    opts.EnableSensitiveDataLogging(true);
});
builder.Services.AddControllersWithViews(); // Controller +view ��� ����
builder.Services.AddRazorPages(); // Razor Pages ��� ����  .cshtml

var app = builder.Build(); // ��� �������� app ��ü ����

app.MapGet("/", () => "Hello World!"); // MapControllers  , MapGet �Ѵ� �����ص�����    
 
app.UseStaticFiles();// ���� ����� ���� 
app.MapControllers(); // ��Ʈ�ѷ� ��� ��û ó�� ����
app.MapControllerRoute("controllers", "controllers/{controller=Home}/{action=Index}/{id?}");
// MVC ����� ���� ����
app.MapRazorPages();// Pages ���  Razor ����� Ȱ��
//@page ��Ƽ�긦 ���� .cshtml ���� ó����
app.Run();  