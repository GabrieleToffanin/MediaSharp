using MediatR;

namespace Tester;
public sealed class MediatRHandler : IRequestHandler<MediatRBho, Qualcosa>
{
    private List<Qualcosa> cose = new List<Qualcosa>()
    {
        new Qualcosa()
        {
            Id = 1
        }
    };

    /// <inheritdoc />
    public async Task<Qualcosa> Handle(MediatRBho request, CancellationToken cancellationToken)
    {
        return await Task.Run(() => this.cose.FirstOrDefault(x => x.Id == request.id)!, cancellationToken);
    }
}
