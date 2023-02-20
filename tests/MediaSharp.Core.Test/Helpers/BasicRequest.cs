using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaSharp.Core.Test.Helpers
{
    public record class BasicRequest(int Id) : IRequest<Basic>;
}
