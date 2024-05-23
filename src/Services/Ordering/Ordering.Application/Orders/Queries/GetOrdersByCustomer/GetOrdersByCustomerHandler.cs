
using FluentValidation.Internal;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer;

public class GetOrdersByCustomerQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersByCustomerQuery, GetOrderByCustomerResult>
{
    public async Task<GetOrderByCustomerResult> Handle(GetOrdersByCustomerQuery request, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders.Include(x=>x.OrderItems)
                                      .AsNoTracking()
                                      .OrderBy(x=>x.OrderName.Value)
                                      .Where(x=>x.CustomerId == CustomerId.of(request.customerId))
                                      .ToListAsync();

        return new GetOrderByCustomerResult(orders.ToOrderDtoList());
    }
}
