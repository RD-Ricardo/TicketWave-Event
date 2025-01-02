using Domain.Exceptions;

namespace Domain.Entities
{
    public sealed class TicketCategory
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public int Quantity { get; private set; } 
        public decimal Price { get; private set; }
        public TicketCategory(string name, int quantity, decimal price)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
        }

        public void Update(string name, int quantity, decimal price)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
        }

        public void SubtractQuantity(int quantity)
        {
            if (quantity > Quantity)
            {
                throw new DomainException("Quantity is not available");
            }

            Quantity -= quantity;
        }
    }
}
