using ChatbotApi.Infra.Repositories;
using ChatbotIntegration;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Pobieramy connection string dla SQL Server (upewnij siê, ¿e masz odpowiedni wpis w appsettings.json, np. "SqlServerConnection")
string connectionString = builder.Configuration.GetConnectionString("SqlServerConnection");

// Rejestracja DbContext dla SQL Server
builder.Services.AddDbContext<ChatDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddHttpClient<IChatbotService, AwsChatbotService>(client =>
{
    // Mo¿esz ustawiæ domyœlny adres bazowy, jeœli to ma sens
    client.BaseAddress = new Uri("https://api.aws.example.com/");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
