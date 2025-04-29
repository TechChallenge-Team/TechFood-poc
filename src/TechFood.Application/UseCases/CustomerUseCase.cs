using System;
using System.Threading.Tasks;
using TechFood.Application.Models.Customer;
using TechFood.Application.UseCases.Interfaces;
using TechFood.Domain.Entities;
using TechFood.Domain.Enums;
using TechFood.Domain.Repositories;
using TechFood.Domain.ValueObjects;

namespace TechFood.Application.UseCases
{
    public class CustomerUseCase(
       ICustomerRepository customerRepo

       ) : ICustomerUseCase
    {
        private readonly ICustomerRepository _customerRepo = customerRepo;
        public async Task<AddCustomerItemResult> AddItemAsync(CreateCustomerRequest data)
        {

            var customer = new Customer(
                new Name(data.Name),
                new Email(data.Email),
                new Document(DocumentType.CPF, data.CPF),
                null);

            var item = await _customerRepo.CreateAsync(customer);

            return new()
            {
                Id = Guid.NewGuid(),
            };
        }
    }
}
