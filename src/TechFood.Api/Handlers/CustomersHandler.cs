using Microsoft.AspNetCore.Mvc;
using TechFood.Application.Controllers;
using TechFood.Application.Interfaces.Controller;
using TechFood.Application.Interfaces.DataSource;
using TechFood.Application.Interfaces.Presenter;
using TechFood.Application.Presenters;
using TechFood.Common.DTO.Customer;

namespace TechFood.Api.Handlers
{
    [ApiController()]
    [Route("v1/customers")]
    [Tags("Customers")]
    public class CustomersHandler : ControllerBase
    {
        private readonly ICustomerController _customerController;

        public CustomersHandler(ICustomerDataSource customerDataSource,
                                 IImageUrlResolver imageUrlResolver,
                                 IUnitOfWorkDataSource unitOfWorkDataSource,
                                 IImageDataSource imageDataSource)
        {
            _customerController = new CustomerController(customerDataSource,
                                                         unitOfWorkDataSource);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerPresenter), 200)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCustomerRequestDTO request)
        {
            var result = await _customerController.CreateCustomerAsync(request);

            return Ok(result);
        }

        [HttpGet("{document}")]
        [ProducesResponseType(typeof(CustomerPresenter), 200)]
        public async Task<IActionResult> GetByDocumentAsync(string document)
        {
            var result = await _customerController.GetByDocumentAsync(document);

            return result != null ? Ok(result) : NotFound();
        }
    }
}
