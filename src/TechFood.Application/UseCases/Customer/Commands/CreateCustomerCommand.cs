using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TechFood.Application.Common.Resources;
using TechFood.Domain.Enums;
using TechFood.Domain.Repositories;
using TechFood.Domain.ValueObjects;

namespace TechFood.Application.UseCases.Customer.Commands;

public class CreateCustomerCommand : IRequest<CreateCustomerCommand.Result>
{
    [Required]
    public string CPF { get; set; } = null!;

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    public class Handler(ICustomerRepository repo) : IRequestHandler<CreateCustomerCommand, Result>
    {
        public async Task<Result> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var document = new Document(DocumentType.CPF, request.CPF);

            var cpfExists = await repo.GetByDocument(document.Type, document.Value);
            if (cpfExists != null)
            {
                throw new Common.Exceptions.ApplicationException(Exceptions.Customer_CpfAlreadyExists);
            }

            var customer = new Domain.Entities.Customer(
                new Name(request.Name),
                new Email(request.Email),
                document,
                null
            );

            var id = await repo.CreateAsync(customer);

            return new()
            {
                Id = id,
                DocumentType = customer.Document.Type,
                DocumentValue = customer.Document.Value,
                Name = customer.Name.FullName,
                Email = customer.Email.Address,
                Phone = customer.Phone?.Number,
            };
        }
    }

    public class Result
    {
        public Guid Id { get; set; }

        public DocumentType DocumentType { get; set; }

        public string DocumentValue { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Phone { get; set; } = null!;
    }
}
