using AuctionTrading.Application.Models.Seller;
using AuctionTrading.WebHost.Requests.Seller;
using AuctionTrading.WebHost.Responses.Seller;
using AutoMapper;

namespace GradeBookMicroservice.WebHost.Mapping
{
    public class RepresentationProfile : Profile
    {
        public RepresentationProfile()
        {
            CreateMap<SellerModel, SellerShortResponse>();
            CreateMap<SellerModel, SellerDetailedResponse>();
            CreateMap<CreateSellerRequest, CreateSellerModel>();
            CreateMap<CreateSellerModel, SellerShortResponse>();
        }

    }
}