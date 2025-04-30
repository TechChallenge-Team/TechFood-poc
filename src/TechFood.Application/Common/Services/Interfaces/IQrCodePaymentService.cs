using System.Threading.Tasks;
using TechFood.Application.Common.Data;

namespace TechFood.Application.Common.Services.Interfaces
{

    public interface IQrCodePaymentService : IPaymentService
    {
        Task<QrCodePaymentResult> GeneratePaymentAsync(QrCodePaymentRequest request);
    }
}
