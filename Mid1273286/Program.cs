using Microsoft.EntityFrameworkCore;
using Mid1273286.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DressDbContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("db")));
builder.Services.AddControllersWithViews();
var app = builder.Build();
app.UseStaticFiles();
app.MapDefaultControllerRoute();
app.Run();