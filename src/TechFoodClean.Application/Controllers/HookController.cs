using TechFoodClean.Application.Gateway;
using TechFoodClean.Application.Interfaces.Controller;
using TechFoodClean.Application.Interfaces.DataSource;
using TechFoodClean.Application.Interfaces.Service;
using TechFoodClean.Common.DTO.Hook;
using TechFoodClean.Domain.Interfaces.UseCase;
using TechFoodClean.Domain.UseCases;

namespace TechFoodClean.Application.Controllers
{
    public class HookController : IHookController
    {
        private readonly IPaymentUseCase _paymentUseCase;

        public HookController(IUnitOfWorkDataSource unitOfWork,
                             IOrderDataSource orderDataSource,
                             IPreparationDataSource preparationDataSource,
                             IPaymentDataSource paymentDataSource,
                             IProductDataSource productDataSource,
                             IImageDataSource imageDataSource,
                             IPaymentService paymentService,
                             IOrderNumberService orderNumberService)
        {
            var preparationGateway = new PreparationGateway(preparationDataSource, unitOfWork);
            var paymentGateway = new PaymentGateway(unitOfWork, paymentDataSource);
            var productGateway = new ProductGateway(productDataSource, imageDataSource, unitOfWork);
            var orderGateway = new OrderGateway(orderDataSource, unitOfWork);
            var paymentServiceGateway = new PaymentServiceGateway(paymentService);
            var orderNumberServiceGateway = new OrderNumberServiceGateway(orderNumberService);
            _paymentUseCase = new PaymentUseCase(paymentGateway, orderGateway, productGateway,
                paymentServiceGateway, preparationGateway, orderNumberServiceGateway);
        }

        public async Task InvokeAsync(HookRequestDTO request)
        {
            if (!request.Type.Equals("Payment", StringComparison.CurrentCultureIgnoreCase))
                throw new ArgumentException("Invalid hook type.", nameof(request.Type));

            if (Guid.TryParse(request.Data.Id, out Guid id))
            {
                var payment = await _paymentUseCase.GetByOrderIdAsync(id);

                if (payment == null)
                    throw new ApplicationException("Invalid order id");

                await _paymentUseCase.ConfirmAsync(payment.Id);
            }
            else
                throw new ApplicationException("Invalid GUID format for the provided ID.");
        }
    }
}
