using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ToDoListAPI.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<Sistem21TodolistContext>(optionsBuilder =>

optionsBuilder.UseMySql("server=sistemas19.com;database=sistem21_todolist;user=sistem21_todolist;password=listapendientes", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.5.17-mariadb")));

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.Run();
