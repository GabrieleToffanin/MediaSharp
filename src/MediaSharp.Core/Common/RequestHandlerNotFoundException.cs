namespace MediaSharp.Core.Common;

public class RequestHandlerNotFoundException : Exception
{
    public RequestHandlerNotFoundException()
        : base()
    {

    }

    public RequestHandlerNotFoundException(string exceptionMessage)
        : base(exceptionMessage)
    {

    }

    public RequestHandlerNotFoundException(string exceptionMessage, Exception ex)
        : base(exceptionMessage, ex)
    {

    }
}