using System.ComponentModel.DataAnnotations;
using TechFoodClean.Common.DTO.Enums;

namespace TechFoodClean.Common.DTO.Payment
{
    public class CreatePaymentRequestDTO
    {
        [Required]
        public Guid? OrderId { get; set; }

        public PaymentTypeDTO Type { get; set; }
    }
}
