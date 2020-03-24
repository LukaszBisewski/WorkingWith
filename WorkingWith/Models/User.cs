using System;
using System.Collections.Generic;
using System.Text;


namespace WorkingWith.Models
{
    public class User
    {

        public string Email { get; private set; }
        public string Password { get; private set; }
        
        public string FirstName { get; set; }

      
        public string LastName { get; set; }
        public int Age { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public decimal Funds { get; private set; }


        public User(string email, string password)
        {
            SetEmail(email);
            SetPassword(password);
            Activate();
            IncreaseFunds(1000);
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("Wprowadzono niepoprawny adres email.");
            }
            if (Email == email)
            {
                return;
            }

            Email = email;
            MarkAsUpdated();
        }

        public void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Wprowadzono niepoprawne hasło.");
            }
            if (Password == password)
            {
                return;
            }

            Password = password;
            MarkAsUpdated();
        }

        public void SetAge(int age)
        {
            if (age < 13)
            {
                throw new Exception("Musisz mieć powyżej 14 lat.");
            }
            if (Age == age)
            {
                return;
            }

            Age = age;
            MarkAsUpdated();
        }

        public void Activate()
        {
            if (IsActive)
            {
                return;
            }

            IsActive = true;
            MarkAsUpdated();
        }

        public void Deactivate()
        {
            if (!IsActive)
            {
                return;
            }

            IsActive = false;
            MarkAsUpdated();
        }

        public void IncreaseFunds(decimal funds)
        {
            if (funds <= 0)
            {
                throw new Exception("Zgromadzone środki muszą być większe od 0.");
            }

            Funds += funds;
            MarkAsUpdated();
        }

        public void PurchaseOrder(Order order)
        {
            if (!IsActive)
            {
                throw new Exception("Tylko aktywni użytkownicy moga złożyć zamówienie.");
            }

            decimal orderPrice = order.TotalPrice;
            if (Funds - orderPrice < 0)
            {
                throw new Exception("Nie masz wystarczajacych środków.");
            }

            order.Purchase();
            Funds -= orderPrice;
            MarkAsUpdated();
        }

        private void MarkAsUpdated()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}