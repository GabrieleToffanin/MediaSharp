using MediaSharp.Core.Internal;
using MediaSharp.Core.Pipe;
using MediaSharp.Core.Pipe.Core;
using MediaSharp.Core.Pipe.Execution;
using Microsoft.Extensions.DependencyInjection;

namespace MediaSharp.Core.DependencyInjection;

/// <summary>
/// Extensions for DI for enabling the usage of MediaSharp
/// </summary>
public static class MediaSharpInjectionExtensions
{
    /// <summary>
    /// Adds the <see cref="IMediator"/> fundamentals in the container
    /// </summary>
    /// <param name="services"></param>
    public static void UseMediaSharp(this IServiceCollection services)
    {
        services.AddScoped<IMediator, Mediator>();
        services.AddSingleton<MediatorContext>();
    }

    /// <summary>
    /// Enables the usage of the <see cref="IExecutionPipeStep"/> inside MediaSharp
    /// </summary>
    /// <param name="services">Current services.</param>
    /// <param name="build">The <seealso cref="Func{TResult}"/> used for building the context</param>
    public static void RegisterMediaSharpPipeline(
        this IServiceCollection services,
        Func<HandlerExecutionPipeBuilder, IServiceProvider, ExecutionPipeContainer> build)
    {
        services.AddSingleton(sp => build(new HandlerExecutionPipeBuilder(), sp));
    }
}