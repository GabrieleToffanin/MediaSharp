using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaSharp.Core;
using MediaSharp.Core.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace MediaSharp.DependencyInjection
{
    public static class MediaSharpInjectionExtensions
    {
        public static void UseMediaSharp(this IServiceCollection services)
        {
            services.AddScoped<IMediator, Mediator>();
            services.AddScoped<MediatorContext>();
        }
    }
}
