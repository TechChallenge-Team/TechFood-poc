using TechFood.Domain.Enums;

namespace TechFood.Application.Models.Order
{
    public class GetAllOrderResponse
    {
        public int Number { get; set; }
        public OrderStatusType Status { get; set; }
    }
}
