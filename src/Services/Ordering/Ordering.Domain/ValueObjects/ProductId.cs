﻿namespace Ordering.Domain.ValueObjects
{
    public record ProductId
    {
        private ProductId(Guid value) => Value = value;
        public Guid Value { get;  }
        public static ProductId Of(Guid value)
        {
            if (value==Guid.Empty)
            {
                throw new CannotUnloadAppDomainException("ProductId cannot be empty.");
            }
            return new ProductId(value);
        }
    }
}
