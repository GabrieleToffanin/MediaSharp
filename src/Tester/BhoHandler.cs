using MediaSharp.Core;
using MediaSharp.Core.Attributes;
using Tester.Foooooooo.Qualcosa;

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

    public BhoHandler(IFoo foo)
    {

    }

    public async Task<Qualcosa> HandleAsync(Bho request, CancellationToken cancellationToken)
    {
        return await Task.Run(() => this.cose.FirstOrDefault(x => x.Id == request.Id)!, cancellationToken);
    }
}