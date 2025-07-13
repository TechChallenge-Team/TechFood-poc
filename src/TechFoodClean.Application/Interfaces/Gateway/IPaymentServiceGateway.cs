using TechFoodClean.Domain.Entities;

namespace TechFoodClean.Application.Interfaces.Gateway
{

    public interface IPaymentServiceGateway
    {
        Task<QrCodePayment> GenerateQrCodePaymentAsync(IEnumerable<Product> product, Order order);
    }

}
