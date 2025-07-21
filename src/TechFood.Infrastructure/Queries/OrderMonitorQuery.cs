//using Microsoft.EntityFrameworkCore;
//using TechFood.Common.DTO;
//using TechFood.Common.DTO.Enums;
//using TechFood.Common.Entities;
//using TechFood.Infrastructure.Data.Contexts;

//namespace TechFood.Infrastructure.Data.Queries;

//internal class OrderMonitorQuery(TechFoodContext dbContext, IImageUrlResolver imageUrlResolver) : IReadOnlyQuery<GetPreparationMonitorDTO>
//{
//    private readonly TechFoodContext _dbContext = dbContext;
//    private readonly IImageUrlResolver _imageUrlResolver = imageUrlResolver;
//    public async Task<IEnumerable<GetPreparationMonitorDTO>> GetAllAsync()
//    {
//        var preparations = await _dbContext.Preparations
//        .Where(p => p.Status != PreparationStatusTypeDTO.Cancelled)
//        .Join(_dbContext.Orders,
//            prep => prep.OrderId,
//            order => order.Id,
//            (prep, order) => new { prep, order })
//        .SelectMany(po => po.order.Items.Select(item => new { po.prep, item }))
//        .Join(_dbContext.Products,
//            poi => poi.item.ProductId,
//            product => product.Id,
//            (poi, product) => new { poi.prep, poi.item, product })
//        .GroupBy(x => new { x.prep.Id, x.prep.Number, x.prep.Status, x.prep.OrderId })
//        .Select(g => new GetPreparationMonitorDTO
//        {
//            preparationId = g.Key.Id,
//            OrderId = g.Key.OrderId,
//            Number = g.Key.Number,
//            Status = g.Key.Status,
//            Products = g.Select(x => new ProductResultDTO
//            {
//                Name = x.product.Name,
//                ImageUrl = x.product.ImageFileName,
//                Quantity = x.item.Quantity
//            }).ToList()
//        })
//        .ToListAsync();

//        foreach (var prep in preparations)
//        {
//            foreach (var product in prep.Products)
//            {
//                product.ImageUrl = _imageUrlResolver.BuildFilePath(nameof(ProductDTO).ToLower(), product.ImageUrl);
//            }
//        }

//        return preparations;
//    }
//}
