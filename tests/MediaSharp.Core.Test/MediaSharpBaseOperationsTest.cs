using MediaSharp.Core.Test.Helpers;

namespace MediaSharp.Core.Test;

public class MediaSharpBaseOperationsTest
{
    [Fact]
    public async Task MediaSharpGenerator_ExtendsClass_Correctly()
    {
        //Arrange
        BasicHandler handler = new BasicHandler(new MediatorContext());

        //Act

        //Assert
        await Assert.ThrowsAsync<NotImplementedException>(() => handler.HandleAsync(new BasicRequest(1), CancellationToken.None));
    }
}