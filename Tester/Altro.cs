using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaSharp.Core;

namespace Tester
{
    public record Altro(int id) : IRequest<IEnumerable<Qualcosa>>;
}
