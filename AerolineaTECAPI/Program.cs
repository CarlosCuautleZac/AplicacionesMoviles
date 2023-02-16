using AerolineaTECAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddDbContext<Sistem21AerolineadbContext>(optionsBuilder => 
optionsBuilder.UseMySql("server=sistemas19.com;user=sistem21_AerolineaDB;password=sistemas19_;database=sistem21_aerolineadb", 
Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.5.17-mariadb")));

var app = builder.Build();


app.UseRouting();
app.MapControllers();
app.Run();
