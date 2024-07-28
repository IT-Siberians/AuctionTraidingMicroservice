using AuctionTrading.Domain.ValueObject.Base;
using AuctionTrading.Domain.ValueObject.Exception;

namespace AuctionTrading.Domain.ValueObject.Validation
{
    /// <summary>
    /// Defines a method that implements the validation of the string.
    /// </summary>
    internal class StringValidator : IValidator<string>
    {
        /// <summary>
        /// Verifies the string to make sure it is not null, empty or doesn't consists only white-space characters. 
        /// </summary>
        /// <param name="value">A string containing data.</param>
        /// <exception cref="ArgumentNullOrWhiteSpaceException"></exception>
        public void Validate(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
                throw new ArgumentNullOrWhiteSpaceException(ExceptionMessage.NOT_NULL_OR_WHITE_SPACE, nameof(value));
        }
    }
}
