using FrameUp.OrderService.Api.Configuration;
using IdentityService.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityService.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var settings = builder.Configuration.GetSection("Settings").Get<Settings>()!;
        builder.Services.AddSingleton(settings);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });

        builder
            .Services
            .AddDatabaseContext(settings)
            .AddRepositories()
            .AddMassTransit(settings)
            .AddUseCases()
            .AddServices(settings)
            .AddValidators();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Configure the HTTP request pipeline.
        if (bool.TryParse(builder.Configuration.GetSection("https").Value, out var result) && result)
            app.UseHttpsRedirection();

        // Use CORS middleware
        app.UseCors("AllowAll");

        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}