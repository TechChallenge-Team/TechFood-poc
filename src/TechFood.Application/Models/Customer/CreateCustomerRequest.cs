using System.ComponentModel.DataAnnotations;

namespace TechFood.Application.Models.Customer;

public class CreateCustomerRequest
{
    public CreateCustomerRequest(string cPF, string name, string email)
    {
        CPF = cPF;
        Name = name;
        Email = email;
    }

    [Required]
    public string CPF { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; set; }
}
