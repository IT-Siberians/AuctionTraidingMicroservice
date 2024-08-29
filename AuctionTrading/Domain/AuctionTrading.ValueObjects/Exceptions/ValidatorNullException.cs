namespace AuctionTrading.Domain.ValueObjects.Exceptions
{
    /// <summary>
    /// The exception that is thrown when no validation method is specified for the type. 
    /// </summary>
    /// <param name="paramName">The name of the object type in which the current exception occurred.</param>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    internal class ValidatorNullException(string paramName, string message)
        : ArgumentNullException(paramName, message)
    {

    }
}
