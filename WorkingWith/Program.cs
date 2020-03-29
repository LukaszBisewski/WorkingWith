using System;
using WorkingWith.Models;

namespace WorkingWith.Models
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Race race = new Race();
            //race.Begin();

            //Shop shop = new Shop();
            //shop.CompleteOrder();
            //shop.CompletFakeOrder();
            //Console.ReadLine();

            var user = new User("user@email.com", "secret");

            var anotherUser = new
            {
                Id = 1,
                Name = "user",
                Address = new
                {
                    Street = "Krakowska 1",
                    City = "Kraków"
                }
            };

        }
    }
}


