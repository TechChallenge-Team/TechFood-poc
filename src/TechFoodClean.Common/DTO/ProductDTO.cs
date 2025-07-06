using System.ComponentModel.DataAnnotations.Schema;
using TechFoodClean.Common.DTO;

namespace TechFoodClean.Common.Entities;

public class ProductDTO : EntityDTO
{

    public string Name { get; private set; } = null!;

    public string Description { get; private set; } = null!;

    [Column("CategoryId")]
    public Guid CategoryDTOId { get; set; }

    public bool OutOfStock { get; set; }

    public string ImageFileName { get; set; } = null!;

    public decimal Price { get; set; }

}
