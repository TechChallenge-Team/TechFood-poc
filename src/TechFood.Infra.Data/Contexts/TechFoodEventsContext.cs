using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechFood.Domain.Shared.Entities;
using TechFood.Domain.Shared.Interfaces;

namespace TechFood.Infra.Data.Contexts
{
    internal class TechFoodEventsContext(TechFoodContext techFoodContext) : IDomainEventStore
    {
        public Task<IEnumerable<IDomainEvent>> GetDomainEventsAsync()
        {
            // get hold of all the domain events
            var domainEvents = techFoodContext.ChangeTracker.Entries<Entity>()
                .Select(entry => entry.Entity.PopEvents())
                .SelectMany(events => events);

            return Task.FromResult(domainEvents);
        }
    }
}
