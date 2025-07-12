using System.ComponentModel.DataAnnotations;

namespace TechFoodClean.Common.DTO.Customer
{
    public class CreateCustomerRequestDTO
    {
        [Required]
        public string CPF { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
    }
}
