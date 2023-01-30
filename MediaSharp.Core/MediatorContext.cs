using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaSharp.Core
{
    public class MediatorContext
    {
        internal Dictionary<Type, object> RequestHandlers { get; } = new();

        public void Add<TRequest, TResult>(IRequestHandler<TRequest, TResult> handler)
            where TRequest : IRequest<TResult>
            where TResult : class
        {
            RequestHandlers[typeof(TRequest)] = handler;
        }
    }
}
