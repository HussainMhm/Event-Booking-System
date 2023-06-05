using MetaX.Data;
using MetaX.Pages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

// Add Cookie Authentication services
builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", config =>
    {
        config.Cookie.Name = "Admin.Cookie";
        config.LoginPath = "/Admin/Login";
    });

// Add Sql server connection
builder.Services.AddDbContext<MetaxDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnaction")));

// Add LoginModel connection
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
builder.Services.AddScoped<LoginModel>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // <-- Add this
app.UseAuthorization();

app.MapRazorPages();

app.Run();
