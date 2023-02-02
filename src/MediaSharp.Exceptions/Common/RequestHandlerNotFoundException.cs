using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaSharp.Exceptions.Common
{
    public class RequestHandlerNotFoundException : Exception
    {
        public RequestHandlerNotFoundException()
            : base()
        {
            
        }

        public RequestHandlerNotFoundException(string exceptionMessage)
            : base(exceptionMessage)
        {
            
        }

        public RequestHandlerNotFoundException(string exceptionMessage, Exception ex)
            : base(exceptionMessage, ex)
        {
            
        }
    }
}
