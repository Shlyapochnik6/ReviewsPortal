namespace ReviewsPortal.Application.Common.Exceptions;

public class ExistingUserException : Exception
{
    public ExistingUserException() : 
        base("User with this email already exists") { }
}