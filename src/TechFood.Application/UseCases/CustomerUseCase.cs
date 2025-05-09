using System;
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
        var document = new Document(DocumentType.CPF, data.CPF);

        var cpfExists = await _customerRepository.GetByDocument(document.Type, document.Value);
        if (cpfExists != null)
        {
            throw new ApplicationException("Já existe um cliente com esse CPF.");
        }
        
        var customer = new Customer(
            new Name(data.Name),
            new Email(data.Email),
            document,
            null
        );

        var item = await _customerRepository.CreateAsync(customer);

        return new()
        {
            Id = item,
        };
    }

    public async Task<CustomerResponse> GetByDocumentAsync(string documentType, string documentValue)
    {
        var document = new Document((DocumentType)Enum.Parse(typeof(DocumentType), documentType), documentValue);

        var customer = await _customerRepository.GetByDocumentAsync(document.Type, document.Value);
        if (customer == null)
        {
            return null;
        }

        return new CustomerResponse
        {
            Id = customer.Id,
            Name = customer.Name.FullName,
            Email = customer.Email.Address,
            DocumentType = customer.Document.Type.ToString(),
            DocumentValue = customer.Document.Value,
        };
    }
    
   
}
