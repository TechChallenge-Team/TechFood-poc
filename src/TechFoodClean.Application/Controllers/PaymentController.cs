using TechFoodClean.Application.Gateway;
using TechFoodClean.Application.Interfaces.Controller;
using TechFoodClean.Application.Interfaces.DataSource;
using TechFoodClean.Application.Interfaces.Service;
using TechFoodClean.Application.Presenters;
using TechFoodClean.Common.DTO.Payment;
using TechFoodClean.Domain.Interfaces.UseCase;
using TechFoodClean.Domain.UseCases;

namespace TechFoodClean.Application.Controllers;

public class PaymentController : IPaymentController
{
    private readonly IPaymentUseCase _paymentUseCase;

    public PaymentController(IUnitOfWorkDataSource unitOfWork,
                             IOrderDataSource orderDataSource,
                             IPreparationDataSource preparationDataSource,
                             IPaymentDataSource paymentDataSource,
                             IProductDataSource productDataSource,
                             IImageDataSource imageDataSource,
                             IPaymentService paymentService,
                             IOrderNumberService orderNumberService)
    {
        var paymentGateway = new PaymentGateway(unitOfWork,
                                                paymentDataSource);
        var productGateway = new ProductGateway(productDataSource, imageDataSource, unitOfWork);
        var orderGateway = new OrderGateway(orderDataSource, unitOfWork);
        var paymentServiceGateway = new PaymentServiceGateway(paymentService);
        var preparationGateway = new PreparationGateway(preparationDataSource, unitOfWork);
        var orderNumberServiceGateway = new OrderNumberServiceGateway(orderNumberService);

        _paymentUseCase = new PaymentUseCase(paymentGateway,
                                             orderGateway,
                                             productGateway,
                                             paymentServiceGateway,
                                             preparationGateway,
                                             orderNumberServiceGateway);
    }

    public async Task<int> ConfirmAsync(Guid id)
    {
        return await _paymentUseCase.ConfirmAsync(id);
    }

    public async Task<PaymentPresenter?> CreateAsync(CreatePaymentRequestDTO createPaymentRequestDTO)
    {
        var result = await _paymentUseCase.CreateAsync(createPaymentRequestDTO);

        return result is not null ?
                   PaymentPresenter.Create(result) :
                   null;
    }

    public async Task<PaymentPresenter?> GetByIdAsync(Guid id)
    {
        var result = await _paymentUseCase.GetByIdAsync(id);

        return result is not null ?
                   PaymentPresenter.Create(result) :
                   null;
    }
}
