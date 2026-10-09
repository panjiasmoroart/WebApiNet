using Microsoft.EntityFrameworkCore;
using WebApiNet.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// https://localhost:7223/openapi/v1.json

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Konfigurasi EF (Entity Framework) Core dengan SQLite
var carConnectionString = builder.Configuration.GetConnectionString("CarDB");
builder.Services.AddDbContext<CarDbContext>(options =>
	options.UseSqlite(carConnectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// gambar, bisa melayani request file statis, misal gambar, css, js, dll. Jadi nanti kita bisa mengakses file gambar yang diupload melalui URL
app.UseStaticFiles();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
	var dbContext = scope.ServiceProvider.GetRequiredService<CarDbContext>();
	dbContext.Database.EnsureCreated(); // Membuat database jika belum ada
}

app.Run();
