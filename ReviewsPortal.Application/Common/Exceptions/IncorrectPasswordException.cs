namespace ReviewsPortal.Application.Common.Exceptions;

public class IncorrectPasswordException : Exception
{
    public IncorrectPasswordException() :
        base($"The entered password isn't " +
             $"consistent with the user password") { }
}