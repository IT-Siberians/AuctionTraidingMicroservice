using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionTrading.Domain.Interfaces;
using AuctionTrading.Domain.ValueObject;

namespace AuctionTrading.Domain.Entities
{
    public class Seller : ISeller
    {
        public Guid Id { get; }
        public string Username { get; private set; }
        public Seller(Guid id, string name)
        {
            Id = id;
            Username = name;
        }
        public bool ChangeName(string name)
        {
            throw new NotImplementedException();
        }

        public Task CancelLotAsync(AuctionLot lot)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AuctionLot>> GetAllLotsAsync(IEnumerable<AuctionLot> lots)
        {
            throw new NotImplementedException();
        }

        public Task<Bid> GetLastBidAsync(Guid lotId, IEnumerable<Bid> bids)
        {
            throw new NotImplementedException();
        }

        public Task<AuctionLot> GetLotDetailsAsync(Guid lotId, IEnumerable<AuctionLot> lots)
        {
            throw new NotImplementedException();
        }
    }
}
