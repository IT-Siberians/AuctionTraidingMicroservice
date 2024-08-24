using AuctionTrading.Domain.ValueObject.Base;
using AuctionTrading.Domain.ValueObject.Validation;

namespace AuctionTrading.Domain.ValueObject
{
    /// <summary>
    /// Represents type of the entity's title.
    /// </summary>
    /// <param name="title">The title of the entity.</param>
    public class Title(string title) : ValueObject<string>(new StringValidator(), title)
    {

    }
}
