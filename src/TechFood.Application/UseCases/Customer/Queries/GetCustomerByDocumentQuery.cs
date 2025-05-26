using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TechFood.Application.QueryProvider;
using TechFood.Domain.Enums;

namespace TechFood.Application.UseCases.Customer.Queries;

public class GetCustomerByDocumentQuery : IRequest<GetCustomerByDocumentQuery.Result?>
{
    [Required]
    public DocumentType DocumentType { get; set; }

    [Required]
    public string DocumentValue { get; set; } = null!;

    public class Handler(ICustomerQueryProvider queries) : IRequestHandler<GetCustomerByDocumentQuery, Result?>
    {
        public Task<Result?> Handle(GetCustomerByDocumentQuery request, CancellationToken cancellationToken)
            => queries.GetByDocumentAsync(request);
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
