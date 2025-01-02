using Domain.Exceptions;

namespace Domain.ValueObjects
{
    public class Address
    {
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string ZipCode { get; private set; }
        public string Country { get; private set; } 
        public string? Complement { get; private set; }

        public Address(string street, string number, string city, string state, string zipCode, string country, string? complement = null)
        {
            Validate(street, number, city, state, zipCode, country);

            Street = street;
            Number = number;
            City = city;
            State = state;
            ZipCode = zipCode;
            Country = country;
            Complement = complement;
        }

        private static void Validate(string street, string number, string city, string state, string zipCode, string country)
        {
            if (string.IsNullOrWhiteSpace(street)) throw new DomainException("Street is required.");
            if (string.IsNullOrWhiteSpace(number)) throw new DomainException("Number is required.");
            if (string.IsNullOrWhiteSpace(city)) throw new DomainException("City is required.");
            if (string.IsNullOrWhiteSpace(state)) throw new DomainException("State is required.");
            if (string.IsNullOrWhiteSpace(zipCode)) throw new DomainException("ZipCode is required.");
            if (string.IsNullOrWhiteSpace(country)) throw new DomainException("Country is required.");
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Street, Number, City, State, ZipCode, Country, Complement);
        }

        public override string ToString()
        {
            return $"{Street}, {Number}, {City} - {State}, {Country}, {ZipCode}";
        }
    }
}
