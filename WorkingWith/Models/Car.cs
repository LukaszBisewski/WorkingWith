using System;
using System.Collections.Generic;
using System.Text;

namespace WorkingWith.Models
{
    public abstract class Car
    {
        public double Aceeleration { get; protected set; } = 10;
        public double Speed { get; protected set; } = 100;

        public void Start()
        {
            Console.WriteLine("Uruchamianie silnika...");
            Console.WriteLine($"Aktualna prędkość: {Speed} km/h.");
        }

        public void Stop()
        {
            Console.WriteLine("Zatrzymywanie maszyny...");
        }

        public virtual void Accelerate()
        {
            Console.WriteLine("Przyśpieszanie...");
            Speed += Aceeleration;
            Console.WriteLine($"Aktualna prędkość: {Speed} km/h.");
        }

        public abstract void Boost();
    }

    public class Truck : Car
    {
        public override void Accelerate()
        {
            Console.WriteLine("Przyśpieszanie ciężarówki...");
            base.Accelerate();
        }

        public override void Boost()
        {
            Console.WriteLine("Zwiększanie prędkości ciężarówki...");
            Speed += 50;
            Console.WriteLine($"Running at: {Speed} km/h.");
        }
    }

    public class SportCar : Car
    {
        public override void Accelerate()
        {
            Console.WriteLine("Przyśpieszenie samochodu sportowego...");
            base.Accelerate();
        }

        public override void Boost()
        {
            Console.WriteLine("Zwiększanie prędkości samochodu sportowego...");
            Speed += 100;
            Console.WriteLine($"Running at: {Speed} km/h.");
        }

        public void DisplayInfo()
        {
            Console.WriteLine("Samochód sportowy.");
        }
    }

    public class Race
    {
        public void Begin()
        {
            Car sportCar = new SportCar();
            Car truck = new Truck();

            List<Car> cars = new List<Car>
            {
                sportCar, truck
            };

            foreach (Car car in cars)
            {
                car.Start();
                car.Accelerate();
                car.Boost();
            }
        }

        public void Casting()
        {
            Car sportCar = new SportCar();
            Car truck = new Truck();

            SportCar castedSportCar = sportCar as SportCar;
            if (castedSportCar != null)
            {
                castedSportCar.DisplayInfo();
            }
        }
    }
}