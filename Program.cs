using MetaX.Data;
using MetaX.Pages;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();