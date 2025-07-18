﻿

namespace Ordering.Application.Orders.Queries.GetOrders
{
    public class GetOrdersHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
    {
        public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {
            var pageIndex = query.PaginationRequest.PageIndex;
            var pageSize = query.PaginationRequest.PageSize;
            var totalCount=await dbContext.Orders.LongCountAsync(cancellationToken);
            var orders =await dbContext.Orders.Include(o => o.OrderItems)
                .AsNoTracking()
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new GetOrdersResult(
                new BuildingBlocks.Pagination.PaginatedResult<OrderDto>(
                    pageIndex,
                    pageSize,
                    totalCount,
                    orders.ToOrderDtoList())
                );
                
        }
    }
}
