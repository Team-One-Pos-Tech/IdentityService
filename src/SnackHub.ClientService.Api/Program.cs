using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SnackHub.ClientService.Api.Extensions;

namespace SnackHub.ClientService.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder
            .Services
            .AddDatabaseContext(builder.Configuration)
            .AddRepositories()
            .AddMassTransit(builder.Configuration)
            .AddUseCases()
            .AddServices()
            .AddValidators();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Configure the HTTP request pipeline.
        if (bool.TryParse(builder.Configuration.GetSection("https").Value, out var result) && result)
            app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}