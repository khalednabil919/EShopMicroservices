
namespace Ordering.Domain.ValueObjects
{
    public record CustomerId
    { 
        public Guid Value { get;}
        private CustomerId(Guid value) => Value = value;
        public static CustomerId of(Guid value)
        {
            ArgumentException.ThrowIfNullOrEmpty(value.ToString());
            if (value == Guid.Empty)
                throw new DomainException("CustomerId cannot be empty");

            return new CustomerId(value);
        }
        
    }
}
