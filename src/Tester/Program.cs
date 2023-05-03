
using BenchmarkDotNet.Running;
using Tester.Benchmark;

var summary = BenchmarkRunner.Run<MediaSharpBenchmarks>();

//var bho = new Bho(1);

//await using var scope = CreateContainer().BeginLifetimeScope();
//var mediator = scope.Resolve<IMediator>();

//var result = await mediator.SendAsync(bho, CancellationToken.None);
//result = await mediator.SendAsync(bho, CancellationToken.None);
//result = await mediator.SendAsync(bho, CancellationToken.None);
//result = await mediator.SendAsync(bho, CancellationToken.None);
//result = await mediator.SendAsync(bho, CancellationToken.None);
//result = await mediator.SendAsync(bho, CancellationToken.None);

//Console.WriteLine(result);

//IContainer CreateContainer()
//{
//    var contBuilder = new ContainerBuilder();
//    var services = new ServiceCollection();
//    services.UseMediaSharp();

//    services.RegisterMediaSharpPipeline((builder, sp) =>
//        builder.Build());

//    services.AddScoped<IRequestHandler<Bho, Qualcosa>, BhoHandler>();

//    contBuilder.Populate(services);

//    return contBuilder.Build();
//}