using TechFoodClean.Application.Interfaces.DataSource;
using TechFoodClean.Application.Interfaces.Gateway;
using TechFoodClean.Common.DTO.Enums;
using TechFoodClean.Common.Entities;
using TechFoodClean.Domain.Entities;
using TechFoodClean.Domain.Enums;

namespace TechFoodClean.Application.Gateway
{
    public class PaymentGateway : IPaymentGateway
    {
        private IUnitOfWorkDataSource _unitOfWorkDataSource;
        private IPaymentDataSource _paymentDataSource;

        public PaymentGateway(IUnitOfWorkDataSource unitOfWorkDataSource,
                              IPaymentDataSource paymentDataSource)
        {
            _unitOfWorkDataSource = unitOfWorkDataSource;
            _paymentDataSource = paymentDataSource;
        }

        public async Task<Guid> AddAsync(Payment payment)
        {
            var paymentDto = new PaymentDTO
            {
                Id = payment.Id,
                OrderId = payment.OrderId,
                Type = (PaymentTypeDTO)payment.Type,
                Amount = payment.Amount,
                CreatedAt = payment.CreatedAt,
                IsDeleted = payment.IsDeleted,
                PaidAt = payment.PaidAt,
                Status = (PaymentStatusTypeDTO)payment.Status
            };

            var result = await _paymentDataSource.AddAsync(paymentDto);

            await _unitOfWorkDataSource.CommitAsync();

            return result;
        }

        public async Task<Payment?> GetByIdAsync(Guid id)
        {
            var payment = await _paymentDataSource.GetByIdAsync(id);

            return payment is not null
                ? new Payment(payment.OrderId, (PaymentType)payment.Type, payment.Amount, payment.Id)
                : null;
        }

        public async Task UpdateAsync(Payment payment)
        {
            var paymentDTO = new PaymentDTO
            {
                Amount = payment.Amount,
                Id = payment.Id,
                CreatedAt = payment.CreatedAt,
                IsDeleted = payment.IsDeleted,
                OrderId = payment.OrderId,
                PaidAt = payment.PaidAt,
                Status = (PaymentStatusTypeDTO)payment.Status,
                Type = (PaymentTypeDTO)payment.Type
            };

            await _paymentDataSource.UpdateAsync(paymentDTO);

            await _unitOfWorkDataSource.CommitAsync();
        }
    }
}
