namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasConversion(oid => oid.Value, dbid => OrderItemId.Of(dbid));
            builder.HasOne<Product>().WithMany()
                .HasForeignKey(o => o.ProductId);
            builder.Property(oi=>oi.Quantity).IsRequired();
            builder.Property(oi=>oi.Price).IsRequired();
        }
    }
}
