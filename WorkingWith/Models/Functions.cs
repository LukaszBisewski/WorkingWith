using System;
using System.Threading;

namespace Episode4.Models
{
    public class Delegates
    {
        public delegate void Write(string message);
        public delegate int Add(int x, int y);
        public delegate void Alert(int change);

        public void Test()
        {
            Write writer = WriteMessage;
            writer("Writer delegate.");
            Add adder = AddTwoNumbers;
            var sum = adder(1, 2);
            Console.WriteLine($"Add delegate has returned the sum of the two numbers: {sum}.");
            CheckTemperature(TooLowAlert, OptimalAlert, TooHighAlert);
        }

        public static void WriteMessage(string message)
        {
            Console.WriteLine($"Writer: {message}");
        }

        public static int AddTwoNumbers(int a, int b)
        {
            return a + b;
        }

        public static void TooLowAlert(int change)
        {
            Console.WriteLine($"Temperature is too low (changed by {change}).");
        }

        public static void OptimalAlert(int change)
        {
            Console.WriteLine($"Temperature is optimal (changed by {change}).");
        }

        public static void TooHighAlert(int change)
        {
            Console.WriteLine($"Temperature is too high (changed by {change}).");
        }

        public static void CheckTemperature(Alert tooLow, Alert optimal, Alert tooHigh)
        {
            var temperature = 10;
            var random = new Random();

            while (true)
            {
                var change = random.Next(-5, 5);
                temperature += change;
                Console.WriteLine($"Temperatue is at: {temperature} C.");
                if (temperature <= 0)
                {
                    tooLow(change);
                }
                else if (temperature > 0 && temperature <= 10)
                {
                    optimal(change);
                }
                else
                {
                    tooHigh(change);
                }

                Thread.Sleep(500);
            }
        }
    }

    public class LambdaExpressions
    {
        public delegate void Writer();

        public void Test()
        {
            // Writer writer = Write;
            Action writer = () => Console.WriteLine("Writing...");
            Action<string, int> advancedWriter = (str, num) => Console.WriteLine($"{str}, {num}");
            writer();
            advancedWriter("Hello!", 1);

            Func<int, int, int> adder = (x, y) => x + y;
            var sum = adder(1, 2);
            Console.WriteLine($"Add lambda expression has returned the sum of the two numbers: {sum}.");

            Action<int, string> logger = (t, m) =>
            {
                Console.WriteLine($"Temperature is at: {t}. {m}");
            };

            CheckTemperature(t => logger(t, "Too low!"), t => logger(t, "Optimal."), t => logger(t, "Too high!"));
        }

        public static void Write()
        {
            Console.WriteLine("Writing...");
        }

        public static void CheckTemperature(Action<int> tooLow, Action<int> optimal, Action<int> tooHigh)
        {
            var temperature = 10;
            var random = new Random();

            while (true)
            {
                var change = random.Next(-5, 5);
                temperature += change;
                if (temperature <= 0)
                {
                    tooLow(temperature);
                }
                else if (temperature > 0 && temperature <= 10)
                {
                    optimal(temperature);
                }
                else
                {
                    tooHigh(temperature);
                }

                Thread.Sleep(500);
            }
        }
    }

    public class Events
    {
        public delegate void UpdateStatus(string status);

        public event UpdateStatus StatusUpdated;
        public EventHandler<StatusEventArgs> StatusUpdatedAgain;

        public void Test()
        {

        }

        public void StartUpdatingStatus()
        {
            while (true)
            {
                if (StatusUpdated != null)
                {
                    StatusUpdated($"status with ticks {DateTime.UtcNow.Ticks}");
                }
                StatusUpdated?.Invoke($"status with ticks {DateTime.UtcNow.Ticks}");
                StatusUpdatedAgain?.Invoke(this, new StatusEventArgs("Status event args"));
                Thread.Sleep(500);
            }
        }
    }

    public class StatusEventArgs : EventArgs
    {
        public string Status { get; }
        public StatusEventArgs(string status)
        {
            Status = status;
        }
    }

    public class FunctionalSandbox
    {
        public void Test()
        {
            var delegates = new Delegates();
            delegates.Test();

            var lambdaExpressions = new LambdaExpressions();
            lambdaExpressions.Test();

            var events = new Events();
            events.StatusUpdated += s =>
            {
                Console.WriteLine($"Received: {s}");
            };
            events.StatusUpdated += DisplayStatus;
            events.StatusUpdated += DisplayStatus2;
            events.StatusUpdatedAgain += (sender, eventArgs) =>
            {
                Console.WriteLine($"Event handler callback: {eventArgs.Status}");
            };
            events.StartUpdatingStatus();
        }

        public void DisplayStatus(string status)
        {
            Console.WriteLine($"Received: {status}");
        }

        public void DisplayStatus2(string status)
        {
            Console.WriteLine($"Received again: {status}");
        }
    }
}