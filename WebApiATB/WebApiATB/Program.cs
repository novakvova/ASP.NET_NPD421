using Domain;
using Microsoft.EntityFrameworkCore;
using WebApiATB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("MyAtbConnection")));

builder.Services.AddControllers();

// Додали свагера
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(u =>
    u.AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod());

app.UseAuthorization();

app.MapControllers();

await app.SeedData();

app.Run();
