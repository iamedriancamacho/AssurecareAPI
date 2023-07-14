using AssurecareAPI.Models;
using Microsoft.CognitiveServices.Speech;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Reflection.Metadata;
using Azure;
using System;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

// Add services to the container.
builder.Services.AddDbContext<BrandContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AssurecareCS")));
builder.Services.AddSingleton(sp =>
{
    string? key2 = configuration.GetSection("ApiKeys").GetSection("ApiKey_Cognitive").Value;
    Console.WriteLine("Key2: " + key2);

    var config = SpeechConfig.FromSubscription(key2, "southeastasia");
    config.SpeechRecognitionLanguage = "en-US";
    return new SpeechRecognizer(config);
});
//.GetSection("ApiKeys").GetSection("ApiKey_Cognitive")
//builder.Services.AddSingleton<IConfiguration>();
builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();