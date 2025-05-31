using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TechFood.Application.Common.Resources;
using TechFood.Domain.Repositories;

namespace TechFood.Application.UseCases.Category.Commands;

public class DeleteCategoryCommand(Guid id) : IRequest<Unit>
{
    public Guid Id { get; set; } = id;

    public class Handler(ICategoryRepository repo) : IRequestHandler<DeleteCategoryCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await repo.GetByIdAsync(request.Id);
            if (category == null)
            {
                throw new Common.Exceptions.ApplicationException(Exceptions.Category_CategoryNotFound);
            }

            await repo.DeleteAsync(category);

            return Unit.Value;
        }
    }
}
