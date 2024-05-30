using Microsoft.AspNetCore.Mvc.Razor;
using System.Data;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// �q�]�w�ɤ����o�s���r��
var connectionString = builder.Configuration.GetConnectionString("DBConnectionString");

// �t�m IDbConnection �`�J
builder.Services.AddScoped<IDbConnection>((sp) => new SqlConnection(connectionString));


builder.Services.Configure<RazorViewEngineOptions>(options => {
    options.ViewLocationFormats.Clear();

    options.ViewLocationFormats.Add("/Areas/BackEnd/Views/{1}/{0}" + RazorViewEngine.ViewExtension);
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

app.MapControllerRoute(
    name: "Areas",
    pattern: "{area:exists}/{controller}/{action=Index}/{id?}/");

app.MapControllerRoute(
    name: "API",
    pattern: "{controller}/{action=Index}/{id?}/");

app.Run();
