using AuctionTrading.Domain.ValueObjects.Base;
using AuctionTrading.Domain.ValueObjects.Exceptions;

namespace AuctionTrading.Domain.ValueObjects.Validators
{
    /// <summary>
    /// Defines a method that implements the validation of the string.
    /// </summary>
    public class UsernameValidator : IValidator<string>
    {
        /// <summary>
        /// The Username's max length
        /// </summary>
        public const int MAX_LENGTH = 30;
        /// <summary>
        /// Verifies the string to make sure it is not null, empty or doesn't consists only white-space characters. 
        /// </summary>
        /// <param name="value">A string containing data.</param>
        /// <exception cref="ArgumentNullOrWhiteSpaceException"></exception>
        public void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullOrWhiteSpaceException(ExceptionMessage.NOT_NULL_OR_WHITE_SPACE);
            if (value.Length > MAX_LENGTH) throw new UsernameLongValueException(value, MAX_LENGTH);

        }
    }
}
