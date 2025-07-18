﻿using BuildingBlocks.CQRS;
using Ordering.Application.Data;
using Ordering.Application.Dtos;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
    {
        public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var order = CreateNewOrder(command.Order);
           dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new CreateOrderResult(order.Id.Value);
        }
        private Order CreateNewOrder(OrderDto orderDto)
        {

            var shipAdd = Address.Of(
                orderDto.ShippingAddress.FirstName,
                orderDto.ShippingAddress.LastName,
                orderDto.ShippingAddress.EmailAddress,
                orderDto.ShippingAddress.AddressLine,
                orderDto.ShippingAddress.Country,
                orderDto.ShippingAddress.State,
                orderDto.ShippingAddress.ZipCode);
            var billAdd = Address.Of(
               orderDto.BillingAddress.FirstName,
               orderDto.BillingAddress.LastName,
               orderDto.BillingAddress.EmailAddress,
               orderDto.BillingAddress.AddressLine,
               orderDto.BillingAddress.Country,
               orderDto.BillingAddress.State,
               orderDto.BillingAddress.ZipCode);
            var newOrder = Order.Create(
                OrderId.Of(Guid.NewGuid()),
                CustomerId.Of(orderDto.CustomerId),
                OrderName.Of(orderDto.OrderName),
                shipAdd,
                billAdd,
                Payment.Of(
                     orderDto.Payment.CardName,
                    orderDto.Payment.CardNumber,
                    orderDto.Payment.Expiration,
                    orderDto.Payment.Cvv,
                    orderDto.Payment.PaymentMethod));
            foreach (var item in orderDto.OrderItems)
            {
                newOrder.Add(ProductId.Of(item.ProductId), item.Quantity, item.Price);
            }
            return newOrder;
        }

    }
}
