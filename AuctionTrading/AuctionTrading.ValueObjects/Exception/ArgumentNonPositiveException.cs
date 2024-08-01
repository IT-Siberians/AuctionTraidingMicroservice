namespace AuctionTrading.Domain.ValueObject.Exception
{
    /// <summary>
    /// The exception that is thrown when one of the decimal arguments is non-positive. 
    /// </summary>
    internal class ArgumentNonPositiveException : DomainValidationException
    {
        /// <summary>
        /// Initializes a new instance of a <see cref="ArgumentNonPositiveException"></see> class with a specified error message 
        /// and the name of the parameter that causes this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="paramName">The name of the parameter that caused the current exception.</param>
        public ArgumentNonPositiveException(string? message, string? paramName)
            : base(message, paramName)
        {

        }
    }
}
