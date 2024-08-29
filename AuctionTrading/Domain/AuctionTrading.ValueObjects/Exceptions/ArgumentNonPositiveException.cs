namespace AuctionTrading.Domain.ValueObjects.Exceptions
{
    /// <summary>
    /// The exception that is thrown when one of the decimal arguments is non-positive. 
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="paramName">The name of the parameter that caused the current exception.</param>
    internal class ArgumentNonPositiveException(string message, string paramName)
        : ArgumentException(message, paramName)
    {

    }
}
