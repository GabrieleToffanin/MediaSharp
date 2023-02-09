using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaSharp.Core;
using MediaSharp.Core.Internal;
using MediaSharp.Core.Pipe;
using MediaSharp.Core.Pipe.Core;
using Microsoft.Extensions.DependencyInjection;

namespace MediaSharp.DependencyInjection
{
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
        /// Enables the usage of the <see cref="IHandlerExecutionPipe"/> inside MediaSharp
        /// </summary>
        /// <param name="services">Current services.</param>
        /// <param name="build">The <seealso cref="Func{TResult}"/> used for building the context</param>
        public static void RegisterMediaSharpPipeline(
            this IServiceCollection services, 
            Func<HandlerExecutionPipeBuilder, IServiceProvider, HandlerExecutionPipeContext> build)
        {
            services.AddSingleton<IHandlerExecutionPipeContext>(sp => build.Invoke(new HandlerExecutionPipeBuilder(), sp));
            services.AddScoped<HandlerExecutionPipeContext>();
            services.AddScoped<IHandlerExecutionPipe, HandlerExecutionPipe>();
        }
    }
}
