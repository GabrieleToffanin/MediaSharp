using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaSharp.Core
{
    /// <summary>
    /// Represents the default request that <see cref="IMediator"/> will
    /// send to the corresponding 
    /// </summary>
    /// <typeparam name="TResult">Query type return</typeparam>
    public interface IRequest<TResult>
        where TResult : class
    {
    }
}
