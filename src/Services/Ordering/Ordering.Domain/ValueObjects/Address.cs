

namespace Ordering.Domain.ValueObjects
{
    public record Address
    {
        public string FirstName { get; } = default!;
        public string LastName { get; } = default!;
        public string? EmailName { get; } = default!;
        public string AddressLine { get; } = default!;
        public string Country { get; } = default!;
        public string State { get; } = default!;
        public string ZipCode { get; } = default!;
        protected Address() { }
        private Address(string firstName, string lastName, string emailName, string addressLine, string country, string state, string zipCode)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailName = emailName;
            AddressLine = addressLine;
            Country = country;
            State = state;
            ZipCode = zipCode;
        }
        public static Address of(string firstName, string lastName, string emailName, string addressLine, string country, string state, string zipCode)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(emailName);
            ArgumentException.ThrowIfNullOrWhiteSpace(addressLine);

            return new Address(firstName,lastName,emailName,addressLine,country,state,zipCode);
        }
    }
}
