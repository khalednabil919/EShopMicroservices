using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObjects
{
    public record OrderName
    {
        private const int DefaultLength = 5;
        public string Value { get; } = default!;
        private OrderName(string value) => Value = value;
        public static OrderName of(string value)
        {
            ArgumentException.ThrowIfNullOrEmpty(value.ToString());
            ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, DefaultLength);
            return new OrderName(value);
        }
 
    }
}
