using Microsoft.EntityFrameworkCore;
using AspNetCoreBlazorEmpty.Models;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BlazorTDBContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:ProductConnection"]); // db연결
    opts.EnableSensitiveDataLogging(true);
});
builder.Services.AddControllersWithViews(); // Controller +view 사용 설정
builder.Services.AddRazorPages(); // Razor Pages 사용 설정  .cshtml

var app = builder.Build(); // 빌더 설정으로 app 객체 생성

app.MapGet("/", () => "Hello World!"); // MapControllers  , MapGet 둘다 공존해도도힘    
app.Views
app.UseStaticFiles();// 정적 라우팅 설정 
app.MapControllers(); // 컨트롤러 기반 요청 처리 설정
app.MapControllerRoute("controllers", "controllers/{controller=Home}/{action=Index}/{id?}");
// MVC 라우팅 집적 지정
app.MapRazorPages();// Pages 기반  Razor 라우팅 활성
//@page 디렉티브를 가진 .cshtml 파일 처리됨
app.Run();  