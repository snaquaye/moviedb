namespace MovieDB.Core.Exceptions;

public class NotFound : Exception 
{
    public NotFound(string message) : base(message)
    {
        throw new NotImplementedException();
    }
}