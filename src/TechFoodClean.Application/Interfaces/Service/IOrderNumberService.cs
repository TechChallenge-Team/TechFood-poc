namespace TechFoodClean.Application.Interfaces.Service
{
    public interface IOrderNumberService
    {
        Task<int> GetAsync();
    }
}
