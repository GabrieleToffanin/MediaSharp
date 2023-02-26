using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediaSharp.Core;
using MediaSharp.Core.Model;
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
    services.RegisterMediaSharpPipeline((builder, sp) => builder.AddStep(new ResolvingStep(sp)).Build());

    services.AddScoped<IRequestHandler<Bho, Qualcosa>, BhoHandler>();

    contBuilder.Populate(services);

    return contBuilder.Build();
}

namespace Tester
{
    class ResolvingStep : HandlerExecutionPipeStep
    {
        private readonly IServiceProvider serviceProvider;
        public ResolvingStep(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public override async Task<IRequest<TResult>> ExecuteAsync<TResult>(IRequest<TResult> request, CancellationToken cancellationToken)
        {
            serviceProvider.GetService<IRequestHandler<Bho, Qualcosa>>();

            return request;
        }
    }
}