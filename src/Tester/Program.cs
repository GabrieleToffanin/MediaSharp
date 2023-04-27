using BenchmarkDotNet.Running;
using MediaSharp.Core;
using MediaSharp.Core.Model;
using MediaSharp.Core.Pipe.Core;
using Microsoft.Extensions.DependencyInjection;
using Tester.Benchmark;

var summary = BenchmarkRunner.Run<MediaSharpBenchmarks>();

namespace Tester
{
    class ResolvingStep : IExecutionPipeStep
    {
        private readonly IServiceProvider _serviceProvider;
        public ResolvingStep(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        /// <inheritdoc />
        public async Task<TResult> ExecutePipelineStep<TResult>(
            IRequest<TResult> request,
            ExecutionPipeStepDelegate<TResult> next,
            CancellationToken cancellationToken) where TResult : class
        {
            _serviceProvider.GetService<IRequestHandler<Bho, Qualcosa>>();

            return await next();
        }
    }

    class LoggingStep : IExecutionPipeStep
    {
        private readonly ILogger _logger;
        public LoggingStep(ILogger logger)
        {
            this._logger = logger;
        }

        /// <inheritdoc />
        public async Task<TResult> ExecutePipelineStep<TResult>(
            IRequest<TResult> request,
            ExecutionPipeStepDelegate<TResult> next,
            CancellationToken cancellationToken) where TResult : class
        {
            this._logger.Log("Ti amo tanto amore mio");

            return await next();
        }
    }

    interface ILogger
    {
        void Log(string message);
    }

    class CustomLogger : ILogger
    {
        /// <inheritdoc />
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}