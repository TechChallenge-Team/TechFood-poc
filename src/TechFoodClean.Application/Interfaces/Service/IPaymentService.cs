using TechFoodClean.Common.DTO.Payment;

namespace TechFoodClean.Application.Interfaces.Service
{
    public interface IPaymentService
    {
        Task<QrCodePaymentResultDTO> GenerateQrCodePaymentAsync(QrCodePaymentRequestDTO request);
    }
}
