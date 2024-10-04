namespace AuctionTrading.Domain.ValueObjects.Exceptions
{
    internal class TitleLongValueException(string title, int maxLength)
        : FormatException($"Title length {title} greater than maximum allowed length {maxLength}")
    {
        public string Title => title;
        public int MaxLength => maxLength;
    }
}
