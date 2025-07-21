using System.ComponentModel.DataAnnotations.Schema;
using TechFood.Common.DTO;

namespace TechFood.Common.Entities;

public class ProductDTO : EntityDTO
{

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    [Column("CategoryId")]
    public Guid CategoryDTOId { get; set; }

    public bool OutOfStock { get; set; }

    public string ImageFileName { get; set; } = null!;

    public decimal Price { get; set; }

}
