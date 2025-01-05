using Domain.Exceptions;
using Domain.ValueObjects;
using Domain.Abstractions;

namespace Domain.Entities
{
    public sealed class Event : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public DateTime Date { get; private set; }
        public Address Address { get; private set; }
        public ICollection<TicketCategory> TicketCategories { get; private set; }

        public Event(string name, string? description, DateTime date, Address address)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Date = date;
            CreatedAt = DateTime.UtcNow;
            Address = address;
            TicketCategories = [];
            Validate();
        }

        public void Update(string name, string? description, DateTime date, Address address)
        {
            Name = name;
            Description = description;
            Date = date;
            Address = address;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddTicketCategory(string name, int quantity, decimal price)
        {
            TicketCategories.Add(new TicketCategory(name, quantity, price));
        }

        public void UpdateTicketCategory(Guid id, string name, int quantity, decimal price)
        {
            var ticketCategory = TicketCategories.FirstOrDefault(x => x.Id == id) ??
                throw new DomainException("Ticket category not found");
            
            ticketCategory.Update(name, quantity, price);
        }

        public void RemoveTicketCategory(Guid id)
        {
            var ticketCategory = TicketCategories.FirstOrDefault(x => x.Id == id);

            if (ticketCategory == null)
            {
                throw new DomainException("Ticket category not found");
            }

            TicketCategories.Remove(ticketCategory);
        }

        public void SubtractTickets(Guid id, int quantity)
        {
            var ticketCategory = TicketCategories.FirstOrDefault(x => x.Id == id) ??
                throw new DomainException("Ticket category not found");
            
            ticketCategory.SubtractQuantity(quantity);
        }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new DomainException("Name is required");
            }
            if (Date < DateTime.UtcNow)
            {
                throw new DomainException("Date must be in the future");
            }
            if (Address == null)
            {
                throw new DomainException("Address is required");
            }
            //if (TicketCategories.Count == 0)
            //{
            //    throw new DomainException("At least one ticket category is required");
            //}
        }
    }
}
