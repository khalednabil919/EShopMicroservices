using BuildingBlocks.Pagination;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.Queries.GetOrders;
public class GetOrdersHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var pageNumber = request.PaginationRequest.PageIndex;
        var pageSize = request.PaginationRequest.PageSize;
        var count = await dbContext.Orders.LongCountAsync();

        var orders = await dbContext.Orders
                              .Include(x => x.OrderItems)
                              .OrderBy(x => x.OrderName.Value)
                              .Skip((pageNumber - 1) * pageSize)
                              .Take(pageSize)
                              .ToListAsync();

        return new GetOrdersResult(new PaginatedResult<OrderDto>(pageNumber, pageSize, count, orders.ToOrderDtoList()));
    }
}
