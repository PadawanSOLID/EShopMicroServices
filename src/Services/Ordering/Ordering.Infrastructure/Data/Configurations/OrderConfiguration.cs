﻿using Ordering.Domain.Enums;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasConversion(orderId => orderId.Value, dbId => OrderId.Of(dbId));

            builder.HasOne<Customer>().WithMany().HasForeignKey(o => o.CustomerId).IsRequired();

            builder.HasMany(o => o.OrderItems)
             .WithOne()
             .HasForeignKey(oi => oi.OrderId);
            builder.ComplexProperty(o=>o.OrderName, orderNameBuilder =>
            {
                orderNameBuilder.Property(on => on.Value).HasColumnName(nameof(Order.OrderName)).HasMaxLength(100).IsRequired();
            });

            builder.ComplexProperty(o => o.ShippingAddress, addressBuilder =>
            {
                addressBuilder.Property(a => a.FirstName).HasMaxLength(50).IsRequired();
                addressBuilder.Property(a => a.LastName).HasMaxLength(50).IsRequired();
                addressBuilder.Property(a => a.EmailAddress).HasMaxLength(50);
                addressBuilder.Property(a => a.AddressLine).HasMaxLength(180).IsRequired();
                addressBuilder.Property(a => a.Country).HasMaxLength(50);
                addressBuilder.Property(a => a.State).HasMaxLength(50);
                addressBuilder.Property(a => a.ZipCode).HasMaxLength(5).IsRequired();
            });
            builder.ComplexProperty(o => o.BillingAddress, addressBuilder =>
            {
                addressBuilder.Property(a => a.FirstName).HasMaxLength(50).IsRequired();
                addressBuilder.Property(a => a.LastName).HasMaxLength(50).IsRequired();
                addressBuilder.Property(a => a.EmailAddress).HasMaxLength(50);
                addressBuilder.Property(a => a.AddressLine).HasMaxLength(180).IsRequired();
                addressBuilder.Property(a => a.Country).HasMaxLength(50);
                addressBuilder.Property(a => a.State).HasMaxLength(50);
                addressBuilder.Property(a => a.ZipCode).HasMaxLength(5).IsRequired();
            });
            builder.ComplexProperty(o => o.Payment, paymentBuilder =>
            {
                paymentBuilder.Property(a => a.CardName).HasMaxLength(50);
                paymentBuilder.Property(a => a.CardNumber).HasMaxLength(24).IsRequired();
                paymentBuilder.Property(a => a.Expiration).HasMaxLength(10);
                paymentBuilder.Property(a => a.CVV).HasMaxLength(3);
                paymentBuilder.Property(a => a.PaymentMethod);
            });
            builder.Property(o => o.Status).HasDefaultValue(OrderStatus.Draft).HasConversion(s => s.ToString(), dbstatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbstatus));

            builder.Property(o => o.TotalPrice);
        }
    }
}
