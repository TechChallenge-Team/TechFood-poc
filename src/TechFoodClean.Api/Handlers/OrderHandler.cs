using Microsoft.AspNetCore.Mvc;
using TechFoodClean.Application.Controllers;
using TechFoodClean.Application.Interfaces.Controller;
using TechFoodClean.Application.Interfaces.DataSource;
using TechFoodClean.Common.DTO;

namespace TechFoodClean.Api.Handlers
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

        [HttpPost]
        [ProducesResponseType(typeof(Guid), 200)]
        public async Task<IActionResult> CreateAsync(CreateOrderRequestDTO request)
        {
            var result = await _orderController.CreateOrderAsync(request);

            return Ok(result);
        }

        [HttpPatch]
        public async Task<IActionResult> FinishAsync([FromBody] FinishOrderRequestDTO request)
        {
            await _orderController.FinishAsync(request);

            return Ok();
        }
    }
}
