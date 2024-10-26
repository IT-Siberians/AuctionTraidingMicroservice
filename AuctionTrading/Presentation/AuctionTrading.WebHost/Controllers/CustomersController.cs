using AuctionTrading.Application.Models.Customer;
using AuctionTrading.Application.Services.Abstractions;
using AuctionTrading.WebHost.Requests.Customer;
using AuctionTrading.WebHost.Responses.Customer;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AuctionTrading.WebHost.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomersController(ICustomersApplicationService customersApplicationService,
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
    }
}
