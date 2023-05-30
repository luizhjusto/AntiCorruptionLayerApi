using AntiCorruptionLayer.Adapter;
using AntiCorruptionLayer.Domain;
using AntiCorruptionLayer.Domain.Helpers;
using AntiCorruptionLayer.Domain.Interfaces;
using AntiCorruptionLayer.Facade;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IGitHubAdapter, GitHubAdapter>();
builder.Services.AddTransient<IGitHubFacade, GitHubFacade>();
builder.Services.AddTransient<IHTTPRequestGeneric, HTTPRequestGeneric>();

//Set configuration
Config.GitHubBaseUrl = builder.Configuration.GetValue<string>("GitHubBaseUrl");
Config.GitHubBearer = builder.Configuration.GetValue<string>("GitHubBearer");
Config.GitHubOwner = builder.Configuration.GetValue<string>("GitHubOwner");

builder.Services.AddControllers();
builder.Services.AddHttpClient();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ApiMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
