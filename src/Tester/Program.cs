using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediaSharp.Core;
using MediaSharp.Core.Internal;
using MediaSharp.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Tester;


var cheneso = new Bho(1);

IContainer CreateContainer()
{
    var contBuilder = new ContainerBuilder();
    var services = new ServiceCollection();
    services.UseMediaSharp();
    services.AddScoped<IRequestHandler<Bho, Qualcosa>, BhoHandler>();

    contBuilder.Populate(services);

    return contBuilder.Build();
}

using var containerScope = CreateContainer().BeginLifetimeScope();

var bho = new Bho(1);
_ = containerScope.Resolve<IRequestHandler<Bho, Qualcosa>>();
var mediator = containerScope.Resolve<IMediator>();

var result = await mediator.SendAsync(bho, CancellationToken.None);

Console.WriteLine(result.Id);