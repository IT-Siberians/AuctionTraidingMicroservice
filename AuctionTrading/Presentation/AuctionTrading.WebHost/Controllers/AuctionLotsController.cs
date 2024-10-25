using AuctionTrading.Application.Models.AuctionLot;
using AuctionTrading.Application.Services.Abstractions;
using AuctionTrading.WebHost.Requests.AuctionLot;
using AuctionTrading.WebHost.Responses.AuctionLot;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AuctionTrading.WebHost.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuctionLotsController(IAuctionLotsApplicationService auctionLotsApplicationService,
                                    IMapper mapper) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AuctionLotShortResponse>))]
        public async Task<IActionResult> GetAllAuctionLots()
        {
            var auctionLots = await auctionLotsApplicationService.GetAuctionLotsAsync();
            return Ok(mapper.Map<IEnumerable<AuctionLotShortResponse>>(auctionLots));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuctionLotDetailedResponse))]
        public async Task<IActionResult> GetAuctionLotById(Guid id)
        {
            var auctionLot = await auctionLotsApplicationService.GetAuctionLotByIdAsync(id);
            if (auctionLot is null)
                return NotFound(id);
            return Ok(mapper.Map<AuctionLotDetailedResponse>(auctionLot));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AuctionLotDetailedResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSeller(CreateAuctionLotRequest request)
        {
            var auctionLot = mapper.Map<CreateAuctionLotModel>(request);
            var isCreatedAuctionLot = await auctionLotsApplicationService.CreateAuctionLotAsync(auctionLot);
            if (!isCreatedAuctionLot)
                return BadRequest();
            return Created("", mapper.Map<AuctionLotShortResponse>(auctionLot));

        }
    }
}
