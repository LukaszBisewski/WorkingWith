using System;
using System.Collections.Generic;
using System.Text;

namespace WorkingWith.Models
{
    public class Order
    {
        public int Id { get; }
        public decimal Price { get; private set; }
        public decimal TaxRate { get; } = 0.23M;
        public decimal TotalPrice { get { return (1 + TaxRate) * Price; } }
        public bool IsPurchased { get; private set; }

        public Order(int id, decimal price)
        {
            Id = id;
            if (price <= 0)
            {
                throw new Exception("Cena zamówienia musi być większ niż 0.");
            }
            Price = price;
        }

        public void Purchase()
        {
            if (IsPurchased)
            {
                throw new Exception("Zamówienie juz zostało złożone.");
            }
            IsPurchased = true;
        }
    }
}