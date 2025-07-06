using TechFoodClean.Common.DTO.Enums;

namespace TechFoodClean.Common.DTO.ValueObjects;

public class DocumentDTO
{
    public DocumentTypeDTO Type { get; set; }

    public string Value { get; set; } = string.Empty;

}
