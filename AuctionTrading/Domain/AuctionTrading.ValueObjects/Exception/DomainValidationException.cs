namespace AuctionTrading.Domain.ValueObject.Exception
{
    /// <summary>
    /// The exception that is thrown when one of the arguments provided to the domain layer method is not valid. 
    /// </summary>
    internal class DomainValidationException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of a <see cref="DomainValidationException"></see> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public DomainValidationException(string? message)
            : base(message)
        {

        }
        /// <summary>
        /// Initializes a new instance of a <see cref="DomainValidationException"></see> class with a specified error message 
        /// and the name of the parameter that causes this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="paramName">The name of the parameter that caused the current exception.</param>
        public DomainValidationException(string? message, string? paramName)
            : base(message, paramName)
        {

        }

    }
}
