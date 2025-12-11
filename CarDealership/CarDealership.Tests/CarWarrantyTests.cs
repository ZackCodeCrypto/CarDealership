using System;
using NUnit.Framework;
using CarDealership.Domain;
using CarDealership.Enums;

namespace CarDealership.Tests
{
    [TestFixture]
    public class CarWarrantyTests
    {
        private Car MakeCar()
        {
            return new PetrolCar("ModelA", "BrandB", 2020, 15000m, UsageType.NewCar, 40, 120);
        }

        // AssignWarranty sets both sides of the association
        [Test]
        public void AssignWarranty_AssociatesBothSides()
        {
            var car = MakeCar();
            var warranty = new Warranty(DateTime.Today, DateTime.Today.AddYears(1), "Full");

            car.AssignWarranty(warranty);

            Assert.That(car.Warranty, Is.EqualTo(warranty));
            Assert.That(warranty.Car, Is.EqualTo(car));
        }

        // RemoveWarranty clears both sides
        [Test]
        public void RemoveWarranty_UnlinksBothSides()
        {
            var car = MakeCar();
            var warranty = new Warranty(DateTime.Today, DateTime.Today.AddYears(1), "Full");

            car.AssignWarranty(warranty);
            car.RemoveWarranty();

            Assert.That(car.Warranty, Is.Null);
            Assert.That(warranty.Car, Is.Null);
        }

        // AssignWarranty throws if the car already has a warranty
        [Test]
        public void AssignWarranty_Throws_WhenCarAlreadyHasWarranty()
        {
            var car = MakeCar();
            var w1 = new Warranty(DateTime.Today, DateTime.Today.AddYears(1), "Basic");
            var w2 = new Warranty(DateTime.Today, DateTime.Today.AddYears(2), "Extended");

            car.AssignWarranty(w1);

            Assert.Throws<InvalidOperationException>(() => car.AssignWarranty(w2));
        }

        // AssignWarranty throws if the warranty already belongs to another car
        [Test]
        public void AssignWarranty_Throws_WhenWarrantyBelongsToAnotherCar()
        {
            var car1 = MakeCar();
            var car2 = MakeCar();

            var warranty = new Warranty(DateTime.Today, DateTime.Today.AddYears(1), "Full");

            car1.AssignWarranty(warranty);

            Assert.Throws<InvalidOperationException>(() => car2.AssignWarranty(warranty));
        }

        // Removing when no warranty exists should do nothing (but not crash)
        [Test]
        public void RemoveWarranty_WhenNoneAssigned_DoesNothing()
        {
            var car = MakeCar();

            car.RemoveWarranty();

            Assert.That(car.Warranty, Is.Null);
        }
    }
}
