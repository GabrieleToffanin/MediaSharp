# MediaSharp

## Reflection free implementation of Mediator Pattern, as MediatR does.

Using SourceGenerators for generating usefull code for the library, and deleting some boilerplate code : 
> [GitHub - Mediator Source Generator](https://github.com/GabrieleToffanin/MediaSharp/tree/master/src/MediaSharp.SourceGenerators/Mediator)

You can simply add the MediaSharp library in you DI container, that needs to use `IServiceCollection` at the moment, using `services.UseMediaSharp()` 
that is implemented inside **MediaSharp.DependencyInjection**.

### Creating a request

For creating a request you are able to use any class or record (has support for it) doing something like:
```csharp

public record GetSomethingById(int Id) : IRequest<Something>;

```

> IRequest<TResult> is accepting any type of class inside the generic at ordinal 0

### Creating a RequestHandler

For creating a request handler **here is where the source generator kicks in** you need to create an `IRequestHandler<TRequest, TResult>` like : 
```csharp
///This is a mere example

[CallableHandler]
public partial class SomethingRequestHandler : IRequestHandler<GetSomethingById, Something>
{
  private List<Something> things = new List<Something>()
    {
        new Something()
        {
            Id = 1
        }
    };

  public async Task<Something> HandleAsync(GetSomethingById request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(() => things.FirstOrDefault(x => x.Id == request.Id));
    }
}

```

Thanks to the `CallableHandlerAttribute` the source generator has the mark of where you handler is and can proceed to 
create some boilerplate code for you. Your `IRequestHandler<GetSomethingById, Something>` must be partial so the generator can extend you class
the library won't work if you don't.
