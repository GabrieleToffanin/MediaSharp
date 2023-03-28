using MediaSharp.Core.Pipe.Core;

namespace MediaSharp.Core.Model;

/// <summary>
/// Represents the delegate for the next
/// <see cref="IExecutionPipeStep"/>
/// </summary>
/// <typeparam name="TResponse"></typeparam>
/// <returns></returns>
public delegate Task<TResponse> ExecutionPipeStepDelegate<TResponse>();