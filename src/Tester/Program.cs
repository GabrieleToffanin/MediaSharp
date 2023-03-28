using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediaSharp.Core;
using MediaSharp.Core.Model;
using MediaSharp.Core.Pipe.Core;
using MediaSharp.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Tester;

var cheneso = new Bho(1);

using var scope = CreateContainer().BeginLifetimeScope();
var mediator = scope.Resolve<IMediator>();

var result = await mediator.SendAsync(cheneso, CancellationToken.None);

Console.WriteLine(result.Id);
IContainer CreateContainer()
{
    var contBuilder = new ContainerBuilder();
    var services = new ServiceCollection();
    services.UseMediaSharp();
    services.RegisterMediaSharpPipeline((builder, sp) =>
        builder.AddStep(new ResolvingStep(sp))
               .Build());

    services.AddTransient<IExecutionPipeStep, ResolvingStep>();

    services.AddScoped<IRequestHandler<Bho, Qualcosa>, BhoHandler>();

    contBuilder.Populate(services);

    return contBuilder.Build();
}

namespace Tester
{
    class ResolvingStep : IExecutionPipeStep
    {
        private readonly IServiceProvider serviceProvider;
        public ResolvingStep(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        /// <inheritdoc />
        public async Task<TResult> ExecutePipelineStep<TResult>(
            IRequest<TResult> request,
            ExecutionPipeStepDelegate<TResult> next,
            CancellationToken cancellationToken) where TResult : class
        {
            serviceProvider.GetService<IRequestHandler<Bho, Qualcosa>>();

            return await next();
        }
    }
}