namespace ReviewsPortal.Application.Common.Exceptions;

public class UnfoundException : Exception
{
    public UnfoundException(string name) :
        base($"{name} wasn't found") { }
}