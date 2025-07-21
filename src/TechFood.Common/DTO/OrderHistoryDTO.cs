using TechFood.Common.DTO;
using TechFood.Common.DTO.Enums;

namespace TechFood.Common.Entities;

public class OrderHistoryDTO : EntityDTO
{
    public Guid OrderId { get; set; }
    public DateTime CreatedAt { get; set; }

    public OrderStatusTypeDTO Status { get; set; }
}
