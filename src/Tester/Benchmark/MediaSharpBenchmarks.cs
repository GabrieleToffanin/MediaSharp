using Autofac;
using Autofac.Extensions.DependencyInjection;
using BenchmarkDotNet.Attributes;
using MediaSharp.Core;
using MediaSharp.Core.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Tester.Benchmark;

[MemoryDiagnoser]
public class MediaSharpBenchmarks
{
    private IMediator _mediator;
    private Bho _bho;
    private MediatRBho _mediatrBho;
    private MediatR.IMediator _mediatorR;

    [GlobalSetup]
    public void Qualcosa()
    {
        this._bho = new Bho(1);
        this._mediatrBho = new MediatRBho(1);

        var scope = CreateContainer().BeginLifetimeScope();
        scope.Resolve<IRequestHandler<Bho, Qualcosa>>();
        this._mediator = scope.Resolve<IMediator>();
        this._mediatorR = scope.Resolve<MediatR.IMediator>();

        IContainer CreateContainer()
        {
            var contBuilder = new ContainerBuilder();
            var services = new ServiceCollection();
            services.UseMediaSharp();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

            services.RegisterMediaSharpPipeline((builder, sp) =>
                builder.Build());

            services.AddScoped<IRequestHandler<Bho, Qualcosa>, BhoHandler>();

            contBuilder.Populate(services);

            return contBuilder.Build();
        }
    }

    [Benchmark]
    public async Task<Qualcosa> SendAsyncMediaSharp()
    {
        return await this._mediator.SendAsync(this._bho, CancellationToken.None);
    }

    //[Benchmark(Baseline = true)]
    //public async Task<Qualcosa> SendAsyncMediatR()
    //{
    //    return await this._mediatorR.Send(this._mediatrBho, CancellationToken.None);
    //}


}
