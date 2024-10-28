using AuctionTrading.Application.Models.Bid;
using AuctionTrading.Application.Models.Customer;
using AuctionTrading.Application.Services.Abstractions;
using AuctionTrading.Domain.ValueObjects;
using AuctionTrading.WebHost.Requests.Bid;
using AuctionTrading.WebHost.Requests.Customer;
using AuctionTrading.WebHost.Responses.AuctionLot;
using AuctionTrading.WebHost.Responses.Bid;
using AuctionTrading.WebHost.Responses.Customer;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AuctionTrading.WebHost.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomersController(ICustomersApplicationService customersApplicationService,
                                    IAuctionLotsApplicationService auctionLotsApplicationService,
                                    IBidderApplicationService bidderApplicationService,
                                    IMapper mapper) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CustomerShortResponse>))]
        public async Task<IActionResult> GetAllCustomers(CancellationToken cancellationToken)
        {
            var customers = await customersApplicationService.GetCustomersAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<CustomerShortResponse>>(customers));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDetailedResponse))]
        public async Task<IActionResult> GetCustomerById(Guid id, CancellationToken cancellationToken)
        {
            var customer = await customersApplicationService.GetCustomerByIdAsync(id, cancellationToken);
            if (customer is null)
                return NotFound(id);
            return Ok(mapper.Map<CustomerDetailedResponse>(customer));
        }

        [HttpGet("{username}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDetailedResponse))]
        public async Task<IActionResult> GetCustomerByUsernameId(string username, CancellationToken cancellationToken)
        {
            var customer = await customersApplicationService.GetCustomerByUsernameAsync(username, cancellationToken);
            if (customer is null)
                return NotFound(username);
            return Ok(mapper.Map<CustomerDetailedResponse>(customer));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CustomerShortResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCustomer(CreateCustomerRequest request, CancellationToken cancellationToken)
        {
            var customer = mapper.Map<CreateCustomerModel>(request);

            var isCreatedCustomer = await customersApplicationService.CreateCustomerAsync(customer, cancellationToken);
            if (!isCreatedCustomer)
                return BadRequest();

            return Created("", mapper.Map<CustomerShortResponse>(customer));
        }

        [HttpPost("Add bid")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BidDetailedResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Guid))]
        public async Task<IActionResult> TryBidOnAuctionLot(CreateBidRequest request, CancellationToken cancellationToken)
        {
            var bid = mapper.Map<CreateBidModel>(request);

            var customer = await customersApplicationService.GetCustomerByIdAsync(bid.CustomerId, cancellationToken);
            var auctionLot = await auctionLotsApplicationService.GetAuctionLotByIdAsync(bid.AuctionLotId, cancellationToken);

            if (auctionLot is null)
                return NotFound(request.AuctionLotId);

            if (auctionLot.SellerId == customer.Id)
                return BadRequest($"the buyer cannot bid on his auction lot with id {auctionLot.Id}");


            var bidStatus = await bidderApplicationService.MakeBidAsync(bid, cancellationToken);
            if (bidStatus != Common.Enums.BidStatus.Success)
                return BadRequest($"An attempt to place a bet ended in failure. Status {bidStatus}");


            if (await customersApplicationService.GetCustomerByIdAsync(customer.Id, cancellationToken) is null)
            {
                await customersApplicationService.CreateCustomerAsync(
                    new CreateCustomerModel(customer.Id, customer.Username),
                    cancellationToken);


                await customersApplicationService.UpdateCustomerAsync(customer, cancellationToken);
            }

            return Created("", mapper.Map<BidDetailedResponse>(customer));

        }
    }
}
