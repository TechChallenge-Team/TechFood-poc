using Microsoft.AspNetCore.Mvc;
using TechFood.Application.Models.Order;
using TechFood.Application.UseCases.Interfaces;

namespace TechFood.Api.Controllers
{
    [ApiController()]
    [Route("v1/[controller]")]
    public class OrdersController(IOrderUseCase orderUseCase) : ControllerBase
    {
        private readonly IOrderUseCase _orderUseCase = orderUseCase;

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateOrderRequest data)
        {
            var result = await _orderUseCase.CreateAsync(data);

            return Ok(result);
        }

        [HttpPost]
        [Route("{orderId:guid}/items")]
        public async Task<IActionResult> AddItemAsync(Guid orderId, AddOrderItemRequest data)
        {
            var result = await _orderUseCase.AddItemAsync(orderId, data);

            return Ok(result);
        }

        [HttpDelete]
        [Route("{orderId:guid}/items/{itemId:guid}")]
        public async Task<IActionResult> RemoveItemAsync(Guid orderId, Guid itemId)
        {
            await _orderUseCase.RemoveItemAsync(orderId, itemId);

            return Ok();
        }

        [HttpPost]
        [Route("{orderId:guid}/payment")]
        public async Task<IActionResult> CreatePaymentAsync(Guid orderId, CreatePaymentRequest data)
        {
            await _orderUseCase.CreatePaymentAsync(orderId, data);

            return Ok();
        }

        [HttpPost]
        [Route("{orderId:guid}/prepare")]
        public async Task<IActionResult> PrepareAsync(Guid orderId)
        {
            await _orderUseCase.PrepareAsync(orderId);

            return Ok();
        }

        [HttpPost]
        [Route("{orderId:guid}/finish")]
        public async Task<IActionResult> FinishAsync(Guid orderId)
        {
            await _orderUseCase.FinishAsync(orderId);

            return Ok();
        }
    }
}
