using AuctionTrading.Application.Models.AuctionLot;
using AuctionTrading.Application.Services.Abstractions;
using AuctionTrading.Common.Infrastructure.Queues.Abstraction;
using AuctionTrading.Domain.Repositories.Abstractions;
using Otus.QueueDto.Lot;

namespace AuctionTrading.Application.Services
{
    public class SellingApplicationService(ISellersRepository sellersRepository, IAuctionLotRepository lotsRepository, IProducerService<CancelLotEvent> cancelLotProducer)
        : ISellingApplicationService
    {
        public async Task<bool> CancelAuctionLotAsync(AuctionLotModel information, CancellationToken cancellationToken = default)
        {
            var seller = await sellersRepository.GetByIdAsync(information.SellerId, cancellationToken);
            if (seller is null)
                return false;

            var lot = await lotsRepository.GetByIdAsync(information.Id, cancellationToken);
            if (lot is null)
                return false;

            if (!seller.CancelLot(lot))
                return false;

            if (!await sellersRepository.UpdateAsync(seller, cancellationToken))
                return false;

            var lotEvent = new CancelLotEvent
            (
                information.SellerId,
                information.Id,
                information.Title,
                information.Description,
                information.StartPrice,
                information.BidIncrement,
                information.RepurchasePrice,
                information.StartDate,
                information.EndDate
            );

            cancelLotProducer.Send(lotEvent);

            return true;
        }
    }
}
