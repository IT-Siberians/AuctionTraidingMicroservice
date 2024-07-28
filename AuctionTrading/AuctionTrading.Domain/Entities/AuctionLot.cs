using AuctionTrading.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionTrading.Domain.Entities
{
    public class AuctionLot
    {
        [Required]
        public Guid Id { get; private set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Title { get; private set; }
        public string Description { get; private set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "StartPrice must be greater than zero.")]
        public decimal StartPrice { get; private set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "FixedBid must be greater than zero.")]
        public decimal FixedBid { get; private set; }
        public decimal? RepurchasePrice { get; private set; }

        [Required(ErrorMessage = "StartDate is required.")]
        public DateTime StartDate { get; private set; }

        [Required(ErrorMessage = "EndDate is required.")]
        public DateTime EndDate { get; private set; }

        [Required]
        public LotStatus Status { get; private set; }

        [Required]
        public Seller Seller { get; private set; }
        public List<Bid> Bids { get; private set; } = new List<Bid>();

        public AuctionLot(Guid id, string title, string description, decimal startPrice, decimal fixedBid, decimal? repurchasePrice, DateTime startDate, DateTime endDate, LotStatus status, Seller seller, List<Bid> bids)
        {
            if (repurchasePrice.HasValue && repurchasePrice < startPrice)
            {
                throw new ArgumentException("RepurchasePrice must be greater than startPrice.");
            }

            if (endDate <= startDate)
            {
                throw new ArgumentException("EndDate must be greater than StartDate.");
            }

            Id = id;
            Title = title;
            Description = description;
            StartPrice = startPrice;
            FixedBid = fixedBid;
            RepurchasePrice = repurchasePrice;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
            Seller = seller;
            Bids = bids;

            Validate();
        }
        public void ChangeStatus(LotStatus newLotStatus)
        {
            throw new NotImplementedException();
        }
        public void AddBid(Bid bid)
        {
            if (Status == LotStatus.Canceled || Status == LotStatus.Completed)
                throw new InvalidOperationException("Cannot bid on a cancelled or finished lot.");

            Bids.Add(bid);
        }
        private void Validate()
        {
            var validationContext = new ValidationContext(this);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(this, validationContext, validationResults, true))
            {
                throw new ValidationException(string.Join(", ", validationResults));
            }
        }
    }
}
