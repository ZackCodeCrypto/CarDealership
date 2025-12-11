using CarDealership.Domain;
using CarDealership.Enums;

namespace CarDealership.Tests
{
    [TestFixture]
    public class AccessoryCarAssociationTests
    {
        [Test]
        public void AddAccessory_FromCarSide_SetsBothSidesAndIsIdempotent()
        {
            var car = new PetrolCar("Corolla", "Toyota", 2020, 20000m, UsageType.NewCar, 50.0, 120);
            var accessory = new Accessory("GPS", "Electronics", 500m);

            car.AddAccessory(accessory);

            Assert.That(car.Accessories, Has.Member(accessory));
            Assert.That(accessory.Car, Is.SameAs(car));
            Assert.That(car.Accessories.Count(a => ReferenceEquals(a, accessory)), Is.EqualTo(1));

            Assert.DoesNotThrow(() => car.AddAccessory(accessory));
            Assert.That(car.Accessories.Count(a => ReferenceEquals(a, accessory)), Is.EqualTo(1));
        }

        [Test]
        public void AssignToCar_FromAccessorySide_ThrowsDueToCurrentRecursionBehaviour()
        {
            var car = new PetrolCar("Civic", "Honda", 2019, 18000m, UsageType.NewCar, 45.0, 110);
            var accessory = new Accessory("Dashcam", "Electronics", 150m);

            Assert.Throws<InvalidOperationException>(() => accessory.AssignToCar(car));
        }

        [Test]
        public void RemoveAccessory_FromCarSide_ClearsBothSides()
        {
            var car = new PetrolCar("Focus", "Ford", 2018, 15000m, UsageType.UsedCar, 55.0, 100);
            var accessory = new Accessory("Roof Rack", "Utility", 250m);

            car.AddAccessory(accessory);
            Assert.That(accessory.Car, Is.SameAs(car));
            Assert.That(car.Accessories, Has.Member(accessory));

            car.RemoveAccessory(accessory);

            Assert.That(car.Accessories, Does.Not.Contain(accessory));
            Assert.That(accessory.Car, Is.Null);
        }

        [Test]
        public void RemoveFromCar_FromAccessorySide_ClearsBothSides()
        {
            var car = new PetrolCar("Impreza", "Subaru", 2021, 22000m, UsageType.NewCar, 60.0, 130);
            var accessory = new Accessory("Tow Hitch", "Utility", 300m);

            car.AddAccessory(accessory);

            accessory.RemoveFromCar();

            Assert.That(car.Accessories, Does.Not.Contain(accessory));
            Assert.That(accessory.Car, Is.Null);
        }

        [Test]
        public void UpdatingAccessory_PropertyIsVisibleThroughCarCollection()
        {
            var car = new PetrolCar("Mustang", "Ford", 2022, 45000m, UsageType.NewCar, 61.0, 300);
            var accessory = new Accessory("Premium Sound", "Electronics", 1200m);

            car.AddAccessory(accessory);

            accessory.Price = 999m;

            var fromCar = car.Accessories.First(a => ReferenceEquals(a, accessory));
            Assert.That(fromCar.Price, Is.EqualTo(999m));
        }
    }
}
