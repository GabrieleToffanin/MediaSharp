using MediatR;

namespace Tester;

public record MediatRBho(int id) : IRequest<Qualcosa>;
