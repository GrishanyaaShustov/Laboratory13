namespace Car
{
    public class MyObservableCollectionTests
    {
        private MyObservableCollection<Car> _collection;
        private bool _collectionChangedCalled;

        [SetUp]
        public void Setup()
        {
            _collection = new MyObservableCollection<Car>();
            _collectionChangedCalled = false;

            _collection.CollectionChanged += (sender, args) =>
            {
                _collectionChangedCalled = true;
            };
        }

        [Test]
        public void Add_Car_ShouldIncreaseCount_AndTriggerEvent()
        {
            var car = new LightCar { Brand = "BMW", MaxSpeed = 150 };
            _collection.Add(car);

            Assert.AreEqual(1, _collection.Count);
            Assert.IsTrue(_collectionChangedCalled, "CollectionChanged event not triggered.");
        }

        [Test]
        public void Remove_Car_ShouldDecreaseCount_AndTriggerEvent()
        {
            var car = new DeliveryCar { Brand = "Ford", Clearance = 12};
            _collection.Add(car);
            _collectionChangedCalled = false;

            bool removed = _collection.Remove(car);

            Assert.IsTrue(removed);
            Assert.AreEqual(0, _collection.Count);
            Assert.IsTrue(_collectionChangedCalled, "CollectionChanged event not triggered.");
        }

        [Test]
        public void Clear_ShouldRemoveAllCars_AndTriggerEvent()
        {
            _collection.Add(new BigCar { Brand = "MAN", AllWheelDrive = true});
            _collection.Add(new LightCar { Brand = "VW", MaxSpeed = 120 });
            _collectionChangedCalled = false;

            _collection.Clear();

            Assert.AreEqual(0, _collection.Count);
            Assert.IsTrue(_collectionChangedCalled, "CollectionChanged event not triggered.");
        }

        [Test]
        public void Indexer_GetSet_WorksCorrectly()
        {
            var car1 = new LightCar { Brand = "Audi", MaxSpeed = 100 };
            var car2 = new LightCar { Brand = "Mercedes", MaxSpeed = 200 };

            _collection.Add(car1);
            _collection[0] = car2;

            Assert.AreEqual("Mercedes", _collection[0].Brand);
        }
    }
}