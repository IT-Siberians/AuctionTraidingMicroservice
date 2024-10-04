using AuctionTrading.Domain.ValueObjects.Base;
using AuctionTrading.Domain.ValueObjects.Exceptions;

namespace AuctionTrading.Domain.ValueObjects.Validators
{
    /// <summary>
    /// Defines a method that implements the validation of the string.
    /// </summary>
    public class UsernameValidator : ITextValidator
    {
        /// <summary>
        /// The Username's max length
        /// </summary>
        public int? MAX_LENGTH => 30;

        public int? MIN_LENGTH => null;

        /// <summary>
        /// Verifies the string to make sure it is not null, empty or doesn't consists only white-space characters. 
        /// </summary>
        /// <param name="value">A string containing data.</param>
        /// <exception cref="ArgumentNullOrWhiteSpaceException"></exception>
        public void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullOrWhiteSpaceException(ExceptionMessages.USERNAME_NOT_NULL_OR_WHITE_SPACE, nameof(value));
            if (value.Length > MAX_LENGTH)
                throw new UsernameLongValueException(value, MAX_LENGTH.Value);
        }
    }
}
