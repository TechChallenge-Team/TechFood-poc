using Microsoft.AspNetCore.Mvc;
using TechFood.Application.Controllers;
using TechFood.Application.Interfaces.Controller;
using TechFood.Application.Interfaces.DataSource;
using TechFood.Application.Presenters;
using TechFood.Common.DTO;

namespace TechFood.Api.Handlers
{
    [ApiController]
    [Route("v1/order")]
    [Tags("Order")]
    public class OrderHandler : ControllerBase
    {
        private readonly IOrderController _orderController;

        public OrderHandler(IOrderDataSource orderDataSource,
            IProductDataSource productDataSource,
            IPreparationDataSource preparationDataSource,
            IUnitOfWorkDataSource unitOfWorkDataSource
            )
        {
            _orderController = new OrderController(orderDataSource, productDataSource
                , preparationDataSource, unitOfWorkDataSource);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderPresenter>), 200)]
        public async Task<IActionResult> GetAllDoneAndInPreparationAsync()
        {
            var result = await _orderController.GetAllDoneAndInPreparationAsync();

            return Ok(result);
        }

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(typeof(OrderPresenter), 200)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _orderController.GetById(id);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrderPresenter), 200)]
        public async Task<IActionResult> CreateAsync(CreateOrderRequestDTO request)
        {
            var result = await _orderController.CreateOrderAsync(request);

            return Ok(result);
        }

        [HttpPatch("{orderId:Guid}/finish")]
        public async Task<IActionResult> FinishAsync(Guid orderId)
        {
            await _orderController.FinishAsync(orderId);

            return Ok("Order finished successfully.");
        }
    }
}
