using Ordering.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObjects
{
    public record  CustomerId
    {
        private CustomerId(Guid value)=>Value = value;
        public Guid Value { get; }
        public static CustomerId Of(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new DomainException("CustomerId cannot be empty.");
            }
            return new CustomerId(value);
        }
    }
}
