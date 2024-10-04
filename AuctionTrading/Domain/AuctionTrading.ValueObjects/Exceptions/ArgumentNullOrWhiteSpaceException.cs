namespace AuctionTrading.Domain.ValueObjects.Exceptions
{
    /// <summary>
    /// The exception that is thrown when one of the string arguments is null, empty or consists only of white-space characters. 
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="paramName">The name of the parameter that caused the current exception.</param>
    internal class ArgumentNullOrWhiteSpaceException(string paramName, string message)
        : ArgumentNullException(paramName, message);
}
