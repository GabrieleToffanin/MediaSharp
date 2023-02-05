using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaSharp.Core.Internal;

namespace MediaSharp.Core
{
    public class MediatorContext
    {
        public Dictionary<Type, IWrappableHandler> RequestHandlers { get; } = new();

        public void Add<TRequest, TResult>(IRequestHandler<TRequest, TResult> handler)
            where TRequest : IRequest<TResult>
            where TResult : class
        {
            RequestHandlers[typeof(TRequest)] = handler;
        }
    }
}
