using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionTrading.Domain.Entities
{
    public class Bid
    {
        public Guid Id { get; private set; }
        public DateTime Timestamp { get; private set; }
        public decimal Amount { get; private set; }
        public AuctionLot Lot { get; private set; }
        public Customer Customer { get; private set; }
        public Bid(Guid id, DateTime timestamp, decimal amount, AuctionLot lot, Customer customer)
        {
            Id = id;
            Timestamp = timestamp;
            Amount = amount;
            Lot = lot;
            Customer = customer;

            Validate();
        }

        private void Validate()
        {
            if (Id == Guid.Empty)
                throw new ArgumentException("Id cannot be empty.", nameof(Id));

            if (Timestamp == default)
                throw new ArgumentException("Timestamp must be a valid date.", nameof(Timestamp));

            if (Amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.", nameof(Amount));

            if (Lot == null)
                throw new ArgumentNullException(nameof(Lot), "Auction lot cannot be null.");

            if (Customer == null)
                throw new ArgumentNullException(nameof(Customer), "Customer cannot be null.");
        }
    }
}
