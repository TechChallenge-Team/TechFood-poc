using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechFoodClean.Application.Interfaces.DataSource;

namespace TechFoodClean.Application.Gateway
{
    public class MenuGateway
    {
        private readonly ICategoryDataSource _categoryDataSource;
        private readonly IProductDataSource _productDataSource;
        private readonly IUnitOfWorkDataSource _unitOfWorkDataSource;
        public MenuGateway(ICategoryDataSource categoryDataSource,
                                 IProductDataSource productDataSource,
                                 IUnitOfWorkDataSource unitOfWorkDataSource)
        {
            _categoryDataSource = categoryDataSource;
            _productDataSource = productDataSource;
            _unitOfWorkDataSource = unitOfWorkDataSource;
        }
    }
}
