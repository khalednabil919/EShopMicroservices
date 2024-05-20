using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Ordering.Infrastructure.Data.Interceptors
{
    public class DispatchDomainEventsInterceptor(IMediator mediator) : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();
            return base.SavingChanges(eventData, result);
        }
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            await DispatchDomainEvents(eventData.Context);

            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public async Task DispatchDomainEvents(DbContext? context)
        {
            if (context is null)
                return;

            var aggregates = context.ChangeTracker.Entries<IAggregate>()
                .Where(x => x.Entity.DomainEvents.Any()).Select(c => c.Entity);

            var DomainEvents = aggregates.SelectMany(x => x.DomainEvents).ToList();
            aggregates.ToList().ForEach(x => x.ClearDomainEvents());

            foreach (var domain in DomainEvents)
                await mediator.Publish(domain);
        }
    }
}
