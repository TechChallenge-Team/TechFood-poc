using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechFood.Application.Models.Preparation;

namespace TechFood.Application.UseCases.Interfaces
{
    public interface IPreparationUseCase
    {
        Task FinishAsync(Guid id);

        Task<IEnumerable<GetPreparationResult>> GetAllAsync();

        Task<GetPreparationResult> GetByIdAsync(Guid id);

        Task StartAsync(Guid id);
    }
}
