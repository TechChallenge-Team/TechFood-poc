namespace TechFoodClean.Domain.Interfaces.Gateway
{
    public interface IOrderNumberServiceGateway
    {
        Task<int> GetAsync();
    }
}
