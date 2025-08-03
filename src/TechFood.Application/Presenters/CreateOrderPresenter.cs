using TechFood.Domain.Entities;

namespace TechFood.Application.Presenters;

public class CreateOrderPresenter
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal Amount { get; set; }
    public decimal Discount { get; set; }

    public static CreateOrderPresenter Create(Order order)
    {
        return new CreateOrderPresenter
        {
            Id = order.Id,
            CreatedAt = order.CreatedAt,
            Amount = order.Amount,
            Discount = order.Discount
        };
    }
}
