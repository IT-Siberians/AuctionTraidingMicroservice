using AuctionTrading.Domain.ValueObjects.Base;
using AuctionTrading.Domain.ValueObjects.Exceptions;

namespace AuctionTrading.Domain.ValueObjects.Validators
{
    /// <summary>
    /// Defines a method that implements the validation of the string.
    /// </summary>
    public class TitleValidator : IValidator<string>
    {
        /// <summary>
        /// The Title's max length
        /// </summary>
        public const int MAX_LENGTH = 50;
        /// <summary>
        /// Verifies the string to make sure it is not null, empty or doesn't consists only white-space characters. 
        /// </summary>
        /// <param name="value">A string containing data.</param>
        /// <exception cref="ArgumentNullOrWhiteSpaceException"></exception>
        public void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullOrWhiteSpaceException(nameof(value), ExceptionMessage.TITLE_NOT_NULL_OR_WHITE_SPACE);
            if (value.Length > MAX_LENGTH)
                throw new TitleLongValueException(value, MAX_LENGTH);
        }
    }
}
