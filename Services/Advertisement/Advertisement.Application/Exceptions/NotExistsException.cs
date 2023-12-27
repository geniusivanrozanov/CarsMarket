namespace Advertisement.Application.Exceptions;

public class NotExistsException : Exception
{
    public NotExistsException(string message) : base(message)
    {
    }
}
