namespace Chat.Application.Exceptions;

public class InvalidOperationException : Exception
{
    public InvalidOperationException(string message) : base(message)
    {
    }
}
