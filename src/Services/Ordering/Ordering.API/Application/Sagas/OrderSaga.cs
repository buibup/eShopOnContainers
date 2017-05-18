﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopOnContainers.Services.Ordering.Domain.AggregatesModel.OrderAggregate;
using Microsoft.eShopOnContainers.Services.Ordering.Infrastructure;

namespace Ordering.API.Application.Sagas
{
    public abstract class OrderSaga : Saga<Order>
    {
        private OrderingContext _orderingContext;

        public OrderSaga(OrderingContext orderingContext) : base(orderingContext)
        {
            _orderingContext = orderingContext;
        }

        public override Order FindSagaById(int id)
        {
            var order = _orderingContext.Orders
                .Include(c => c.OrderStatus)
                .Include(c => c.OrderItems)
                .Single(c => c.Id == id);

            return order;
        }

        public override async Task<bool> SaveChangesAsync()
        {
            return await _orderingContext.SaveEntitiesAsync();
        }
    }
}