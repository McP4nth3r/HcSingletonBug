using System.Collections.Concurrent;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace HotChocolateSingletonBug;

public class GraphQlService
{
    private IServiceProvider _serviceProvider = null!;
    private bool _servicesInitialized;
    private WebApplication _webApplication = null!;

    public Task StartAsync()
    {
        if (!_servicesInitialized)
            throw new InvalidOperationException("Cannot start server without initializing services!");

        Task.Run(async () =>
        {
            Console.WriteLine("Starting ArtiZen GraphQL Server...");
            await _webApplication.RunAsync();
        }).Wait();

        return Task.CompletedTask;
    }

    public Task InitServicesAsync(CancellationToken cancellationToken = default)
    {
        if (_servicesInitialized)
            throw new InvalidOperationException("Cannot initialize services twice!");

        var builder = WebApplication.CreateBuilder();
        builder.Services
            .AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policyBuilder =>
                    {
                        policyBuilder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
        builder.Services.AddSingleton(typeof(IDictionary<string, Guid>), typeof(ConcurrentDictionary<string, Guid>));
        builder.Services
            .AddGraphQLServer()
            .AddQueryType(q => q.Name(OperationTypeNames.Query))
            .AddExtendObjectTypesWithName(OperationTypeNames.Query);

        _serviceProvider = builder.Services.BuildServiceProvider();

        _webApplication = builder.Build();
        _webApplication.UseWebSockets();
        _webApplication.UseCors();
        _webApplication.UseStaticFiles();
        _webApplication.MapGraphQL();
        _servicesInitialized = true;
        return Task.CompletedTask;
    }

    public IServiceProvider GetServiceProvider()
    {
        return _serviceProvider;
    }
}