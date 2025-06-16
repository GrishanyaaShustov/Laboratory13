using System;
using System.Linq; // Для First()

class Program
{
    static void Main()
    {
        var collection1 = new MyObservableCollection<Car.Car>(0, car => car.CarId.Number);
        var collection2 = new MyObservableCollection<Car.Car>(0, car => car.CarId.Number);

        var journal1 = new Journal("Журнал 1");
        var journal2 = new Journal("Журнал 2");

        collection1.CollectionCountChanged += journal1.OnCollectionChanged;
        collection1.CollectionReferenceChanged += journal1.OnCollectionChanged;

        collection2.CollectionCountChanged += journal2.OnCollectionChanged;
        collection2.CollectionReferenceChanged += journal2.OnCollectionChanged;

        bool running = true;
        while (running)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Добавить случайный элемент в коллекцию 1");
            Console.WriteLine("2. Добавить случайный элемент в коллекцию 2");
            Console.WriteLine("3. Удалить первый элемент из коллекции 1");
            Console.WriteLine("4. Удалить первый элемент из коллекции 2");
            Console.WriteLine("5. Заменить первый элемент в коллекции 1");
            Console.WriteLine("6. Заменить первый элемент в коллекции 2");
            Console.WriteLine("7. Показать коллекции");
            Console.WriteLine("8. Показать журналы");
            Console.WriteLine("0. Выход");
            Console.Write("Выбор: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    var car1 = new Car.Car();
                    car1.RandomInit();
                    collection1.Add(car1);
                    break;

                case "2":
                    var car2 = new Car.Car();
                    car2.RandomInit();
                    collection2.Add(car2);
                    break;

                case "3":
                    if (collection1.Any())
                    {
                        var firstCar = collection1.First();
                        collection1.Remove(firstCar);
                        Console.WriteLine("Элемент удалён.");
                    }
                    else
                        Console.WriteLine("Коллекция пуста.");
                    break;

                case "4":
                    if (collection2.Any())
                    {
                        var firstCar = collection2.First();
                        collection2.Remove(firstCar);
                        Console.WriteLine("Элемент удалён.");
                    }
                    else
                        Console.WriteLine("Коллекция пуста.");
                    break;

                case "5":
                    if (collection1.Any())
                    {
                        var oldCar = collection1.First();
                        var newCar = new Car.Car();
                        newCar.RandomInit();
                        collection1[oldCar] = newCar;
                        Console.WriteLine("Элемент заменён.");
                    }
                    else
                        Console.WriteLine("Коллекция пуста.");
                    break;

                case "6":
                    if (collection2.Any())
                    {
                        var oldCar = collection2.First();
                        var newCar = new Car.Car();
                        newCar.RandomInit();
                        collection2[oldCar] = newCar;
                        Console.WriteLine("Элемент заменён.");
                    }
                    else
                        Console.WriteLine("Коллекция пуста.");
                    break;

                case "7":
                    Console.WriteLine("\nКоллекция 1:");
                    collection1.Print();
                    Console.WriteLine("\nКоллекция 2:");
                    collection2.Print();
                    break;

                case "8":
                    Console.WriteLine();
                    journal1.Print();
                    Console.WriteLine();
                    journal2.Print();
                    break;

                case "0":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Неверный ввод.");
                    break;
            }
        }
    }
}
