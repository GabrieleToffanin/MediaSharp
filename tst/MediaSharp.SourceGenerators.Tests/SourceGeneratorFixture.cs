using MediaSharp.Core;
using MediaSharp.Core.DependencyInjection;
using MediaSharp.SourceGenerators.Tests.Common;
using Microsoft.Extensions.DependencyInjection;

namespace MediaSharp.SourceGenerators.Tests;

public class SourceGeneratorFixture : IAsyncLifetime
{
    protected IMediator Mediator { get; private set; }
    
    public async Task InitializeAsync()
    {
        // Configura il container DI
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<IRequestHandler<TestRequest, TestSomething>, TestHandler>();
        serviceCollection.AddScoped<ITestRepository, DummyRepository>();
        serviceCollection.UseMediaSharp();
        serviceCollection.RegisterMediaSharpPipeline((builder, sp) =>
        {
            return builder.Build();
        });

        // Crea il provider
        var serviceProvider = serviceCollection.BuildServiceProvider();
        
        Mediator = serviceProvider.GetRequiredService<IMediator>();

        // Esegui eventuali inizializzazioni asincrone, se necessarie
        await Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        Mediator = null;
    }
}