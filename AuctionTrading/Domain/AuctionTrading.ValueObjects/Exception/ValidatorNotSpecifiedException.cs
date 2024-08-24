namespace AuctionTrading.Domain.ValueObject.Exception
{
    /// <summary>
    /// The exception that is thrown when no validation method is specified for the type. 
    /// </summary>
    internal class ValidatorNotSpecifiedException : DomainValidationException
    {
        /// <summary>
        /// Initializes a new instance of a <see cref="ValidatorNotSpecifiedException"></see> class with a specified error message 
        /// and the name of the object type that causes this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="objectType">The name of the object type in which the current exception occurred.</param>
        public ValidatorNotSpecifiedException(string? message, string? objectType)
            : base(message, objectType)
        {

        }
    }
}
