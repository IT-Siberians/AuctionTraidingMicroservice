namespace AuctionTrading.Domain.Exception
{
    internal static class ExceptionMessage
    {
        public const string CANNOT_CANCEL_LOT_ANOTHER_SELLER = "Cannot cancel a lot that belongs to another seller";
        public const string CANNOT_CANCEL_NOT_ACTIVE_LOT = "Cannot cancel a lot that does not have an active status";
        public const string CANNOT_CANCEL_LOT_EMPTY_SEQUENCE = "Сannot cancel a lot because the seller's lot sequence is empty";
        public const string CANNOT_GET_LOT_ANOTHER_SELLER = "Сannot get a lot that belongs to another seller";
        public const string CANNOT_GET_LOT_EMPTY_SEQUENCE = "Сannot get a lot because the seller's lot sequence is empty";
        public const string CANNOT_GET_NOT_ACTIVE_LOT = "Сannot get a lot that does not have an active status";


    }
}
