using AuctionTrading.Application.Models.Seller;
using AuctionTrading.Domain.Entities;
using AutoMapper;

namespace GradeBookMicroservice.Application.Services.Mapping
{
    public class SellerMapping : Profile
    {
        public SellerMapping()
        {
            CreateMap<Seller, SellerModel>().ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username.Value));
        }

    }
}