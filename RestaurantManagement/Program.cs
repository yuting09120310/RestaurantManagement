using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.OpenApi.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Swagger 設定
var key = Encoding.ASCII.GetBytes("MySuperSecretKeyThatIsVeryLongAndSecure12345!");
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "餐廳API",
        Description = "Your API Description"
    });
});

// 將服務新增至容器中
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// 從設定檔中取得連接字串
var connectionString = builder.Configuration.GetConnectionString("DBConnectionString");

// 設定 IDbConnection 注入
builder.Services.AddScoped<IDbConnection>((sp) => new SqlConnection(connectionString));

builder.Services.Configure<RazorViewEngineOptions>(options => {
    options.ViewLocationFormats.Clear();

    options.ViewLocationFormats.Add("/Areas/BackEnd/Views/{1}/{0}" + RazorViewEngine.ViewExtension);
    options.ViewLocationFormats.Add("/Areas/BackEnd/Views/Shared/{0}" + RazorViewEngine.ViewExtension);
    options.ViewLocationFormats.Add("/FrontEnd/Views/{1}/{0}" + RazorViewEngine.ViewExtension);
    options.ViewLocationFormats.Add("/FrontEnd/Views/Shared/{0}" + RazorViewEngine.ViewExtension);
    options.ViewLocationFormats.Add("/API/Views/{0}" + RazorViewEngine.ViewExtension);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

// 啟用 Swagger 中介軟體
app.UseSwagger(c =>
{
    c.RouteTemplate = "api/swagger/{documentName}/swagger.json";
});

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "餐廳API");
    c.RoutePrefix = "api/swagger";
});


app.MapControllerRoute(
    name: "Areas",
    pattern: "{area:exists}/{controller}/{action=Index}/{id?}/");

app.MapControllerRoute(
    name: "ForntEnd",
    pattern: "{controller=Default}/{action=Index}/{id?}/");

app.Run();