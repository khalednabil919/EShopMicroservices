using MassTransit;
using MassTransit.Testing;
using Microsoft.FeatureManagement;
using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.EventHandlers.Domain
{
    public class OrderCreatedEventHandler
        (IPublishEndpoint publishEndpoint, ILogger<OrderCreatedEventHandler> logger, IFeatureManager featureManager)
        : INotificationHandler<OrderCreatedEvent>
    {
        public async Task Handle(OrderCreatedEvent orderEvent, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event handled: {DomainEvent}", orderEvent.GetType());

            if (await featureManager.IsEnabledAsync("OrderFullfilment"))
            {
                var orderCreatedIntegrationEvent = orderEvent.order.ToOrderDTO();
                await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
            }
        }
    }
}
