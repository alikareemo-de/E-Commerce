using ECommerce.Application.Commands;
using ECommerce.Application.DTOs;
using ECommerce.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{

    [ApiController]
    [Route("orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetOrderById), new { id = result.OrderId }, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(Guid id)
        {
            var result = await _mediator.Send(new GetOrderByIdQuery(id));
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelOrder(Guid id, [FromQuery] string reason = "Canceled by user")
        {
            var command = new CancelOrderCommand
            {
                OrderId = id,
                CancellationReason = reason
            };

            await _mediator.Send(command);
            return NoContent();
        }
    }
}
