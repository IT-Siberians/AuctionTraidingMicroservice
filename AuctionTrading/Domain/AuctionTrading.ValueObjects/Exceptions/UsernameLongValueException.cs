namespace AuctionTrading.Domain.ValueObjects.Exceptions
{
    internal class UsernameLongValueException(string username, int maxLength)
        : FormatException($"Username length {username} greater than maximum allowed length {maxLength}")
    {
        public string Username => username;
        public int MaxLength => maxLength;
    }
}