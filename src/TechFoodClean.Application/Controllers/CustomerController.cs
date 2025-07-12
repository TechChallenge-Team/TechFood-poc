using TechFoodClean.Application.Gateway;
using TechFoodClean.Application.Interfaces.Controller;
using TechFoodClean.Application.Interfaces.DataSource;
using TechFoodClean.Application.Interfaces.UseCase;
using TechFoodClean.Application.Presenters;
using TechFoodClean.Application.UseCases;
using TechFoodClean.Common.DTO.Customer;

namespace TechFoodClean.Application.Controllers
{
    public class CustomerController : ICustomerController
    {
        private readonly ICustomerUseCase _customerUseCase;
        public CustomerController(ICustomerDataSource customerDataSource,
                                  IUnitOfWorkDataSource unitOfWorkDataSource)
        {
            var customerGateway = new CustomerGateway(customerDataSource, unitOfWorkDataSource);
            _customerUseCase = new CustomerUseCase(customerGateway);
        }
        public async Task<CustomerPresenter?> CreateCustomerAsync(CreateCustomerRequestDTO customerDTO)
        {
            var customer = await _customerUseCase.CreateCustomerAsync(customerDTO);

            return customer is not null ?
                   CustomerPresenter.Create(customer) :
                   null;
        }

        public async Task<CustomerPresenter?> GetByDocumentAsync(string documentValue)
        {
            var customer = await _customerUseCase.GetByDocumentAsync(documentValue);

            return customer is not null ?
                   CustomerPresenter.Create(customer) :
                   null;
        }
    }
}
