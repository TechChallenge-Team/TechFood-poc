using System.Threading.Tasks;
using TechFood.Application.Models.Customer;
using TechFood.Application.UseCases.Interfaces;
using TechFood.Domain.Entities;
using TechFood.Domain.Enums;
using TechFood.Domain.Repositories;
using TechFood.Domain.ValueObjects;

namespace TechFood.Application.UseCases;

internal class CustomerUseCase(
   ICustomerRepository customerRepository
   ) : ICustomerUseCase
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<CreateCustomerResult> CreateCustomerAsync(CreateCustomerRequest data)
    {
        var customer = new Customer(
            new Name(data.Name),
            new Email(data.Email),
            new Document(DocumentType.CPF, data.CPF),
            null);

        var item = await _customerRepository.CreateAsync(customer);

        return new()
        {
            Id = item,
        };
    }
}
