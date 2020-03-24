using System;
using System.Collections.Generic;
using System.Text;

namespace WorkingWith.Models
{
    public interface IEmailSender
    {
        void SendMessage(string receiver, string title, string message);
    }

    public class EmailSender : IEmailSender
    {
        public void SendMessage(string receiver, string title, string message)
        {
            Console.WriteLine("Sending a real email message...");
        }
    }

    public interface IDatabase
    {
        bool IsConnected { get; }
        void Connect();
        User GetUser(string email);
        Order GetOrder(int id);
        void SaveChanges();
    }

    public class Database : IDatabase
    {
        public bool IsConnected { get; private set; }

        public void Connect()
        {
            Console.WriteLine("Połączenie do bazy danych...");
            IsConnected = true;
        }

        public Order GetOrder(int id)
        {
            return new Order(1, 100);
        }

        public User GetUser(string email)
        {
            return new User(email, "secret");
        }

        public void SaveChanges()
        {
            Console.WriteLine("Zapisywanie zmian w bazie danych...");
        }
    }

    public interface IOrderProcessor
    {
        void ProcessOrder(string email, int orderId);
    }

    public class OrderProcessor : IOrderProcessor
    {
        private readonly IDatabase _database;
        private readonly IEmailSender _emailSender;

        public OrderProcessor(IDatabase database, IEmailSender emailSender)
        {
            _database = database;
            _emailSender = emailSender;
        }

        public void ProcessOrder(string email, int orderId)
        {
            User user = _database.GetUser(email); //Fetch from db
            Order order = _database.GetOrder(orderId); //Fetch from db
            Console.WriteLine($"Przetwarzanie zamówienia o: {orderId} dla użytkownika: '{user.Email}'");
            user.PurchaseOrder(order);
            _database.SaveChanges();
            _emailSender.SendMessage(email, "Zamówienie wykonane.", "Zakupiłeś przedmiot.");
        }
    }

    public class FakeEmailSender : IEmailSender
    {
        public void SendMessage(string receiver, string title, string message)
        {
            Console.WriteLine("Wysyłanie fake emaila...");
        }
    }

    public class FakeDatabase : IDatabase
    {
        public bool IsConnected { get; private set; }

        public void Connect()
        {
            Console.WriteLine("Fałszywe połącznie do bazy...");
            IsConnected = true;
        }

        public Order GetOrder(int id)
        {
            return new Order(1, 200);
        }

        public User GetUser(string email)
        {
            return new User($"fake_{email}", "secret");
        }

        public void SaveChanges()
        {
            Console.WriteLine("Zapisywanie zmian w testowej bazie danych...");
        }
    }

    public class Shop
    {
        public void CompleteOrder()
        {
            IDatabase database = new Database();
            IEmailSender emailSender = new EmailSender();
            IOrderProcessor orderProcessor = new OrderProcessor(database, emailSender);
            orderProcessor.ProcessOrder("user1@email.com", 1);
        }

        public void CompletFakeOrder()
        {
            IDatabase database = new FakeDatabase();
            IEmailSender emailSender = new FakeEmailSender();
            IOrderProcessor orderProcessor = new OrderProcessor(database, emailSender);
            orderProcessor.ProcessOrder("user1@email.com", 1);
        }
    }
}