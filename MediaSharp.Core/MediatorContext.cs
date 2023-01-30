using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaSharp.Core
{
    public class MediatorContext
    {
        public List<object> RequestHandlers { get; set; }

        public void Add<TResult>(IRequestHandler<IRequest<TResult>, TResult> handler)
           where TResult : class
        {
            RequestHandlers.Add(handler);
        }
    }
}
