using FaleMais.Api.Factories;
using FaleMais.Application.Services;
using FaleMais.Domain.Interfaces.Repository;
using FaleMais.Infrastructure.Repositories;
using FaleMaisApplication.Services.Interfaces;


var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();


var builder = WebApplication.CreateBuilder(args);

string provider = builder.Configuration["DatabaseSettings:Provider"] ?? throw new Exception("Database provider is not configured.");

ServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

logger.LogInformation($"Using db provider {provider}");


builder.Services.AddSingleton(serviceProvider => DatabaseFactory.CreateRepository(configuration, provider)); 

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITariffService, TariffService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseCors("AllowAllOrigins");
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();