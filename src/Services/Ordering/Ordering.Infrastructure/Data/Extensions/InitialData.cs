using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Data.Extensions
{
    internal class InitialData
    {
        public static IEnumerable<Customer> Customers =>
            [
                new Customer
                {
                    Id = CustomerId.Of(Guid.NewGuid()),
                    Name = "John Doe",
                    Email = ""}
                ];
    }
}
