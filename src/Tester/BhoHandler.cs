using MediaSharp.Core;
using MediaSharp.Core.Attributes;

namespace Tester;

[CallableHandler]
public partial class BhoHandler : IRequestHandler<Bho, Qualcosa>
{
    private List<Qualcosa> cose = new List<Qualcosa>()
    {
        new Qualcosa()
        {
            Id = 1
        }
    };

    public async Task<Qualcosa> HandleAsync(Bho request, CancellationToken cancellationToken)
    {
        return await Task.Run(() => cose.FirstOrDefault(x => x.Id == request.Id), cancellationToken);
    }
}