using AuctionTrading.Application.Models.Seller;
using AuctionTrading.Application.Services.Abstractions;
using AuctionTrading.WebHost.Requests.Seller;
using AuctionTrading.WebHost.Responses.Seller;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AuctionTrading.WebHost.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SellersController(ISellersApplicationService sellersApplicationService,
                                    ICustomersApplicationService customersApplicationService,
                                    ISellingApplicationService sellingApplicationService,
                                    IAuctionLotsApplicationService lotsApplicationService,
                                    IBidderApplicationService bidderApplicationService,
                                    IMapper mapper) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SellerShortResponse>))]
        public async Task<IActionResult> GetAllSellers()
        {
            var sellers = await sellersApplicationService.GetSellersAsync();
            return Ok(mapper.Map<IEnumerable<SellerShortResponse>>(sellers));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SellerDetailedResponse))]
        public async Task<IActionResult> GetSellerById(Guid id)
        {
            var seller = await sellersApplicationService.GetSellerByIdAsync(id);
            if (seller is null)
                return NotFound(id);
            return Ok(mapper.Map<SellerDetailedResponse>(seller));
        }

        [HttpGet("{username}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SellerDetailedResponse))]
        public async Task<IActionResult> GetSellerByUsernameId(string username)
        {
            var seller = await sellersApplicationService.GetSellerByUsernameAsync(username);
            if (seller is null)
                return NotFound(username);
            return Ok(mapper.Map<SellerDetailedResponse>(seller));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SellerShortResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSeller(CreateSellerRequest request)
        {
            var seller = mapper.Map<CreateSellerModel>(request);
            var isCreatedSeller = await sellersApplicationService.CreateSellerAsync(seller);
            if (!isCreatedSeller)
                return BadRequest();
            return Created("", mapper.Map<SellerShortResponse>(seller));

        }
    }
}
