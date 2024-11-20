using AuctionGrpcClient;
using AuctionTrading.Application.Models.Bid;
using AuctionTrading.Application.Models.Seller;
using AuctionTrading.Application.Services.Abstractions;
using AuctionTrading.Common.Enums;
using AuctionTrading.Common.Infrastructure.Queues.Abstraction;
using AuctionTrading.Common.Responses;
using AuctionTrading.Common.Responses.Base;
using AuctionTrading.Domain.Entities;
using AuctionTrading.Domain.Repositories.Abstractions;
using AuctionTrading.Domain.ValueObjects;
using AuctionTrading.GrpcClient;
using AutoMapper;
using MassTransit;
using Microsoft.VisualBasic;
using Otus.QueueDto.Lot;
using System.Net.Http.Headers;

namespace AuctionTrading.Application.Services
{
    public class BidderApplicationService(
        ICustomersRepository customersRepository,
        IAuctionLotRepository lotsRepository,
        IRepository<Bid, Guid> bidsRepository,
        IProducerService<BidPerLotEvent> lotBidProducer,
        IProducerService<WonLotEvent> lotPurchasedProducer,
        ITradingClient client)
        : IBidderApplicationService
    {
        public async Task<IResponse<BidStatus>> MakeBidAsync(CreateBidModel bidInformation, CancellationToken cancellationToken = default)
        {
            var customer = await customersRepository.GetByIdAsync(bidInformation.CustomerId, cancellationToken);
            if (customer is null)
                return new BidResponse(BidStatus.FaultedCustomerNotFound);

            var lot = await lotsRepository.GetByIdAsync(bidInformation.AuctionLotId, cancellationToken);
            if (lot is null)
                return new BidResponse(BidStatus.FaultedLotNotFound);

            if (!lot.IsActive)
                return new BidResponse(BidStatus.FaultedLotNotActive);

            if (customer.Id == lot.Seller.Id)
                return new BidResponse(BidStatus.FaultedCreateBidOnYourLot);

            bool isFirstBid = lot.LastBid is null;
            Customer? previousCustomer = isFirstBid ? null : lot.LastBid!.Customer;

            var bidStatus = customer.TryMakeBid(lot, new(bidInformation.Amount));

            if (bidStatus == BidStatus.Success)
            {
                var request = new ReserveMoneyCommandGrpc
                {
                    BuyerId = customer.Id.ToString(),
                    Price = (double)bidInformation.Amount,
                    Lot = new LotInfoModelGrpc
                    {
                        Id = lot.Id.ToString(),
                        Title = lot.Title.Value,
                        Description = lot.Description.Value
                    }
                };

                var response = await client.ReserveMoney(request, cancellationToken);
                if (response.IsError == true)
                    return new BidResponse(BidStatus.FaultedIncorrectBid, response.Message);

                var newBid = lot.LastBid;
                var result = await bidsRepository.AddAsync(newBid, cancellationToken);
                if (result is not null)
                {
                    if (lot.IsCompleted)
                    {
                        lotPurchasedProducer.Send(new WonLotEvent
                            (
                            customer.Id,
                            lot.Seller.Id,
                            lot.Id,
                            lot.LastBid!.Amount.Value,
                            lot.Title.Value,
                            lot.Description.Value,
                            lot.StartPrice.Value,
                            lot.BidIncrement.Value,
                            lot.RepurchasePrice.Value,
                            lot.StartDate,
                            lot.EndDate
                            ));
                        var payForLotRequest = new PayForLotCommandGrpc
                        {
                            BuyerId = customer.Id.ToString(),
                            SellerId = lot.Seller.Id.ToString(),
                            LotId = lot.Id.ToString(),
                            HammerPrice = (double)lot.LastBid!.Amount.Value
                        };
                        response = await client.PayForLotAsync(payForLotRequest, cancellationToken);
                        return response.IsError == true
                            ? new BidResponse(BidStatus.FaultedPayForLot, response.Message)
                            : new BidResponse(BidStatus.Success);
                    }
                    var realeaseMoneyRequest = new RealeaseMoneyCommandGrpc
                    {
                        BuyerId = customer.Id.ToString(),
                        LotId = previousCustomer.Id.ToString(),
                        Price = (double)lot.LastBid!.Amount.Value
                    };
                    response = await client.RealeaseMoneyAsync(realeaseMoneyRequest, cancellationToken);
                    if (response.IsError)
                        return new BidResponse(BidStatus.FaultedNotRealeaseMoney);
                    lotBidProducer.Send(new BidPerLotEvent(
                        customer.Id, 
                        previousCustomer is null?Guid.Empty:previousCustomer.Id, 
                        lot.Seller.Id, 
                        lot.Id, 
                        lot.Title.Value, 
                        lot.LastBid!.Amount.Value));
                        return new BidResponse(bidStatus);
                }

                return new BidResponse(BidStatus.FaultedIncorrectBid);
            }

            return new BidResponse(bidStatus);
        }
    }
}
