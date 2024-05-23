using Microsoft.EntityFrameworkCore;
using Ordering.Application.Dtos;
using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.Queries.GetOrdersByName;
public class GetOrdersByNameHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
{
    public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
                                .Include(c => c.OrderItems)
                                .AsNoTracking()
                                .Where(x => x.OrderName.Value.Contains(query.name))
                                .OrderBy(x => x.OrderName.Value)
                                .ToListAsync(cancellationToken);

        
        return new GetOrdersByNameResult(orders.ToOrderDtoList());
    }
 
}
