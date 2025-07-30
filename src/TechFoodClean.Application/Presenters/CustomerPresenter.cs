using TechFoodClean.Common.DTO.Enums;
using TechFoodClean.Domain.Entities;

namespace TechFoodClean.Application.Presenters
{
    public class CustomerPresenter
    {
        public Guid Id { get; set; }
        public DocumentTypeDTO DocumentType { get; set; }
        public string DocumentValue { get; set; } = null!;
        public string? Name { get; set; }
        public string? Email { get; set; }

        public static CustomerPresenter Create(Customer customer)
        {
            return new CustomerPresenter
            {
                DocumentType = (DocumentTypeDTO)customer.Document.Type,
                DocumentValue = customer.Document.Value,
                Id = customer.Id,
                Email = customer.Email?.Address,
                Name = customer.Name?.FullName
            };
        }
    }
}
