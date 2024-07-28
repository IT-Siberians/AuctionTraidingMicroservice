namespace AuctionTrading.Domain.ValueObject.Base
{
    /// <summary>
    /// Defines a method that implements the validation of the object.
    /// </summary>
    /// <typeparam name="T">Type of validation object.</typeparam>
    internal interface IValidator<T>
    {
        /// <summary>
        /// Validates data.
        /// </summary>
        /// <param name="value">The verified value</param>
        void Validate(T value);
    }
}
