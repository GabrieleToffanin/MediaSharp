using Autofac;
using Autofac.Extensions.DependencyInjection;
using BenchmarkDotNet.Attributes;
using MediaSharp.Core;
using MediaSharp.Core.DependencyInjection;
using MediaSharp.Core.Pipe.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Tester.Benchmark;

[MemoryDiagnoser]
public class MediaSharpBenchmarks
{
    private IMediator _mediator;
    private Bho _bho;

    [GlobalSetup]
    public void Qualcosa()
    {
        this._bho = new Bho(1);

        using var scope = CreateContainer().BeginLifetimeScope();
        scope.Resolve<IRequestHandler<Bho, Qualcosa>>();
        this._mediator = scope.Resolve<IMediator>();

        IContainer CreateContainer()
        {
            var contBuilder = new ContainerBuilder();
            var services = new ServiceCollection();
            services.UseMediaSharp();
            services.AddScoped<ILogger, CustomLogger>();
            services.AddTransient<IExecutionPipeStep, ResolvingStep>();
            services.AddTransient<IExecutionPipeStep, LoggingStep>();


            services.RegisterMediaSharpPipeline((builder, sp) =>
                builder.Build());

            services.AddScoped<IRequestHandler<Bho, Qualcosa>, BhoHandler>();

            contBuilder.Populate(services);

            return contBuilder.Build();
        }
    }

    [Benchmark(Baseline = true)]
    public async Task<Qualcosa> TrySendAsync()
    {
        return await this._mediator.SendAsync(this._bho, CancellationToken.None);
    }


}
