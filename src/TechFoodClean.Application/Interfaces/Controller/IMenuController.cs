using TechFoodClean.Application.Presenters;

namespace TechFoodClean.Application.Interfaces.Controller
{
    public interface IMenuController
    {
          Task<MenuPresenter?> GetAsync();
    }
}
