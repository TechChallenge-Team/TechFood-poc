using TechFoodClean.Application.Interfaces.Service;
using TechFoodClean.Domain.Interfaces.Gateway;

namespace TechFoodClean.Application.Gateway
{
    public class OrderNumberServiceGateway : IOrderNumberServiceGateway
    {
        private readonly IOrderNumberService _orderNumberService;
        public OrderNumberServiceGateway(IOrderNumberService orderNumberService)
        {
            _orderNumberService = orderNumberService;
        }
        public Task<int> GetAsync()
        {
            return _orderNumberService.GetAsync();
        }
    }
}
