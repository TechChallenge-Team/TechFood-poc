using TechFoodClean.Common.DTO;
using TechFoodClean.Common.DTO.Enums;

namespace TechFoodClean.Common.Entities;

public class OrderHistoryDTO : EntityDTO
{
    public DateTime CreatedAt { get; set; }

    public OrderStatusTypeDTO Status { get; set; }
}
