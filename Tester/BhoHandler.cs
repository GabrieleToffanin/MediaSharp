using MediaSharp.Core;
using MediaSharp.Core.Attributes;
using MediaSharp.Core.Internal;

namespace Tester;

[CallableHandler]
public partial class BhoHandler : IRequestHandler<Bho, Qualcosa>
{

    public BhoHandler(Altro altro)
    {
        
    }

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