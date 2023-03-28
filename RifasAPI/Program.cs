using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RifasAPI.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSignalR();
string? cadena = builder.Configuration.GetConnectionString("RifasConnectionStrings");

builder.Services.AddDbContext<Sistem21RifasContext>(optionsBuilder 
    => optionsBuilder.UseMySql(cadena, ServerVersion.AutoDetect(cadena)));


var app = builder.Build();

app.MapControllers();
app.Run();
