using MediaSharp.Core;
using MediaSharp.Core.Attributes;

namespace Tester;


public partial class NumbersHandler
{
    public List<Qualcosa> Numbers = new()
    {
        new Qualcosa()
        {
            Id = 1
        }
    };

    public async Task<IEnumerable<Qualcosa>> HandleAsync(Altro request, CancellationToken cancellationToken)
    {
        return Numbers.Where(x => x.Id == request.id);
    }
}