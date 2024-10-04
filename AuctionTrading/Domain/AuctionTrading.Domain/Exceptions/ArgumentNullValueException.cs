using AuctionTrading.Domain.Entities;

namespace AuctionTrading.Domain.Exceptions
{
    public class ArgumentNullValueException(string paramName)
        : ArgumentNullException(paramName, $"Argument \"{paramName}\" value is null");
}
