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
    public static class MediaSharpInjectionExtensions
    {
        public static void UseMediaSharp(this IServiceCollection services)
        {
            services.AddScoped<IMediator, Mediator>();
            services.AddSingleton<MediatorContext>();
        }

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
