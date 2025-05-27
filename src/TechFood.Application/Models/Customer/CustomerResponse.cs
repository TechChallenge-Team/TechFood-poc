using System;
using TechFood.Domain.Enums;

namespace TechFood.Application.Models.Customer;

public class CustomerResponse
{
    public Guid Id { get; set; }
    public DocumentType DocumentType { get; set; }
    public string DocumentValue { get; set; } = null!;
    public string? Name { get; set; }
    public string? Email { get; set; }
}
