using TechFood.Domain.Entities;

namespace TechFood.Application.Presenters;

public class OrderPresenter
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }

    public static OrderPresenter Create(Order order)
    {
        return new OrderPresenter
        {
            Id = order.Id,
            CreatedAt = order.CreatedAt,
        };
    }
}
