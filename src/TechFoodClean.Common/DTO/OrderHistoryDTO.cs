using TechFoodClean.Common.DTO;
using TechFoodClean.Common.DTO.Enums;

namespace TechFoodClean.Common.Entities;

public class OrderHistoryDTO : EntityDTO
{
    public Guid OrderId { get; set; }
    public DateTime CreatedAt { get; set; }

    public OrderStatusTypeDTO Status { get; set; }
}
