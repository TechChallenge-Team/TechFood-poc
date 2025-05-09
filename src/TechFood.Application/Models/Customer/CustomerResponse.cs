using System;

namespace TechFood.Application.Models.Customer;

public class CustomerResponse
{
    public Guid Id { get; set; }
    public string DocumentType { get; set; } = null!;
    public string DocumentValue { get; set; } = null!;
    public string? Name { get; set; }
    public string? Email { get; set; }
}
