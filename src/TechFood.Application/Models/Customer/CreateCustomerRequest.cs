using System.ComponentModel.DataAnnotations;

namespace TechFood.Application.Models.Customer
{
    public class CreateCustomerRequest
    {
        [Required]
        public string CPF { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        
    }
}
