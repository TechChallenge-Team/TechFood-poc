using TechFoodClean.Common.DTO;
using TechFoodClean.Common.DTO.Enums;

namespace TechFoodClean.Common.Entities;

public class OrderDTO : EntityDTO
{
    private readonly List<OrderItemDTO> _items = [];

    private readonly List<OrderHistoryDTO> _historical = [];

    public Guid? CustomerId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? FinishedAt { get; set; }

    public OrderStatusTypeDTO Status { get; set; }

    public decimal Amount { get; set; }

    public decimal Discount { get; set; }

    public IReadOnlyCollection<OrderItemDTO> Items => _items.AsReadOnly();

    public IReadOnlyCollection<OrderHistoryDTO> Historical => _historical.AsReadOnly();

}
