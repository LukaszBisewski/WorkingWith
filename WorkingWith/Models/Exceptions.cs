using System;
using System.Collections.Generic;
using System.Text;

namespace WorkingWith.Models
{
    class Exceptions
    {
        public void Test()
        {
            try
            {
                User user = new User("user1@email.com", "Secret");
                user = null;
                throw new ArgumentNullException(nameof(user));

                //Sign up user...
                //Email in use
                throw new Exception("Adres Email w użyciu.");
            }
            catch (ArgumentNullException exception)
            {
                Console.WriteLine($"Null error: {exception}");
            }
            catch (Exception exception)
            {
                Console.WriteLine($"An error: {exception}");
            }
            finally
            {
                Console.WriteLine("Finally here!");
            }

            Console.WriteLine("OK");
        }
    }
}
