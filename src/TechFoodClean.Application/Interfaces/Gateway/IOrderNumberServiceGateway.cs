namespace TechFoodClean.Application.Interfaces.Gateway
{
    public interface IOrderNumberServiceGateway
    {
        Task<int> GetAsync();
    }
}
