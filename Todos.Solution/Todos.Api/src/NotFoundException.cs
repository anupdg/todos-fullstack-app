using System;

namespace Todos.Api;

public class NotFoundException : ApplicationException
{
    private const string DefaultMessage = "Not found";

    public NotFoundException(string message = DefaultMessage) : base(message) { }

    public NotFoundException(Exception innerException)
        : base(DefaultMessage, innerException) { }

}
