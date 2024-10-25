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
        public async Task<IEnumerable<SellerModel>> GetSellersAsync()
            => (await repository.GetAllAsync()).Select(mapper.Map<SellerModel>);

        public async Task<SellerModel?> GetSellerByIdAsync(Guid id)
        {
            var seller = await repository.GetByIdAsync(id);
            return seller is null ? null : mapper.Map<SellerModel>(seller);
        }
        
        public async Task<SellerModel?> GetSellerByUsernameAsync(string username)
        {
            var seller = await repository.GetSellerByUsernameAsync(username);
            return seller is null ? null : mapper.Map<SellerModel>(seller);
        }
        public async Task<bool> CreateSellerAsync(CreateSellerModel sellerInformation)
        {
            Seller seller = new(sellerInformation.Id, new Username(sellerInformation.Username));
            return await repository.AddAsync(seller);
        }

        public async Task<bool> UpdateSellerAsync(SellerModel seller)
        {
            var entity = await repository.GetByIdAsync(seller.Id);
            if (entity is null)
                return false;
            entity = mapper.Map<Seller>(seller);
            return await repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteSellerAsync(Guid id)
        {
            var seller = await repository.GetByIdAsync(id);
            return seller is null? false: await repository.DeleteAsync(seller);
        }
    }
}
