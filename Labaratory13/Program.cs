class Program
{
    static void Main()
    {
        var collection1 = new MyObservableCollection<Car.Car>();
        var collection2 = new MyObservableCollection<Car.Car>();

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
            Console.WriteLine("3. Удалить элемент из коллекции 1");
            Console.WriteLine("4. Удалить элемент из коллекции 2");
            Console.WriteLine("5. Заменить элемент в коллекции 1");
            Console.WriteLine("6. Заменить элемент в коллекции 2");
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
                    if (collection1.Length > 0)
                        collection1.Remove(collection1[0]);
                    break;
                case "4":
                    if (collection2.Length > 0)
                        collection2.Remove(collection2[0]);
                    break;
                case "5":
                    if (collection1.Length > 0)
                    {
                        var newCar = new Car.Car();
                        newCar.RandomInit();
                        collection1[0] = newCar;
                    }
                    break;
                case "6":
                    if (collection2.Length > 0)
                    {
                        var newCar = new Car.Car();
                        newCar.RandomInit();
                        collection2[0] = newCar;
                    }
                    break;
                case "7":
                    Console.WriteLine("\nКоллекция 1:");
                    collection1.Print();
                    Console.WriteLine("\nКоллекция 2:");
                    collection2.Print();
                    break;
                case "8":
                    journal1.Print();
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