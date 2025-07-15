using TechFoodClean.Domain.Entities;

namespace TechFoodClean.Domain.Interfaces.Gateway
{

    public interface IPaymentServiceGateway
    {
        Task<QrCodePayment> GenerateQrCodePaymentAsync(IEnumerable<Product> product, Order order);
    }

}
