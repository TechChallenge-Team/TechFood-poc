using TechFood.Common.DTO;
using TechFood.Common.DTO.Enums;

namespace TechFood.Common.Entities;

public class PaymentDTO : EntityDTO
{

    public Guid OrderId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? PaidAt { get; set; }

    public PaymentTypeDTO Type { get; set; }

    public PaymentStatusTypeDTO Status { get; set; }

    public decimal Amount { get; set; }
}
