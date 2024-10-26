using AuctionTrading.Application.Models.Seller;
using AuctionTrading.Application.Services.Abstractions;
using AuctionTrading.Domain.Entities;
using AuctionTrading.Domain.Repositories.Abstractions;
using AuctionTrading.Domain.ValueObjects;
using AutoMapper;

namespace AuctionTrading.Application.Services
{
    public class SellersApplicationService(ISellersRepository repository, IMapper mapper) : ISellersApplicationService
    {
        public async Task<IEnumerable<SellerModel>> GetSellersAsync(CancellationToken cancellationToken = default)
            => (await repository.GetAllAsync(cancellationToken, true))
            .Select(mapper.Map<SellerModel>);

        public async Task<SellerModel?> GetSellerByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var seller = await repository.GetByIdAsync(id, cancellationToken);
            var res = mapper.Map<SellerModel>(seller);
            return seller is null ? null : mapper.Map<SellerModel>(seller);
        }

        public async Task<SellerModel?> GetSellerByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            var seller = await repository.GetSellerByUsernameAsync(username, cancellationToken);
            return seller is null ? null : mapper.Map<SellerModel>(seller);
        }
        public async Task<bool> CreateSellerAsync(CreateSellerModel sellerInformation, CancellationToken cancellationToken = default)
        {
            Seller seller = new(sellerInformation.Id, new Username(sellerInformation.Username));
            return await repository.AddAsync(seller, cancellationToken);
        }

        public async Task<bool> UpdateSellerAsync(SellerModel seller, CancellationToken cancellationToken = default)
        {
            var entity = await repository.GetByIdAsync(seller.Id, cancellationToken);
            if (entity is null)
                return false;
            entity = mapper.Map<Seller>(seller);
            return await repository.UpdateAsync(entity, cancellationToken);
        }

        public async Task<bool> DeleteSellerAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var seller = await repository.GetByIdAsync(id, cancellationToken);
            return seller is null ? false : await repository.DeleteAsync(seller, cancellationToken);
        }
    }
}
