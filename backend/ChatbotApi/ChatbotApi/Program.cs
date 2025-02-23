using ChatbotApi.Infra.Repositories;
using ChatbotIntegration;
using Microsoft.EntityFrameworkCore;
using ChatbotApi.Infra.Persistance;
using ChatbotApi.Infra.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
options.AddPolicy("AllowFrontend",
    policy => policy.WithOrigins("http://localhost:4200") // ?? Adres Angulara
        .AllowAnyMethod()
        .AllowAnyHeader());
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// Pobieramy connection string dla SQL Server (upewnij siê, ¿e masz odpowiedni wpis w appsettings.json, np. "SqlServerConnection")
string connectionString = builder.Configuration.GetConnectionString("MySqlConnection");

// Rejestracja DbContext dla SQL Server
builder.Services.AddDbContext<ChatDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();
builder.Services.AddScoped<IConversationRepository, ConversationRepository>();

builder.Services.Configure<HuggingFaceChatbotSettings>(
    builder.Configuration.GetSection("AwsChatbot"));
builder.Services.AddHttpClient<IChatbotService, HuggingFaceChatbotService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowFrontend");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
