using System;
using WorkingWith.Models;

namespace WorkingWith.Models
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Race race = new Race();
            race.Begin();

            Shop shop = new Shop();
            shop.CompleteOrder();
            shop.CompletFakeOrder();
            Console.ReadLine();
        }
    }
}

