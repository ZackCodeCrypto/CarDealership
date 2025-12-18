using System;
using CarDealership.Domain;
using CarDealership.Enums;
using NUnit.Framework;

namespace CarDealership.Tests
{
    [TestFixture]
    public class CarInheritanceTests
    {

        [Test]
        public void PetrolCar_IsCar()
        {
            var car = new PetrolCar("Civic", "Honda", 2020, 20000m, UsageType.NewCar, 45, 140);

            Assert.That(car, Is.InstanceOf<Car>());
            Assert.That(car, Is.InstanceOf<PetrolCar>());
        }

        [Test]
        public void DieselCar_IsCar()
        {
            var car = new DieselCar("Golf", "VW", 2019, 18000m, UsageType.UsedCar, 50, 120);

            Assert.That(car, Is.InstanceOf<Car>());
            Assert.That(car, Is.InstanceOf<DieselCar>());
        }

        [Test]
        public void ElectricCar_ImplementsElectricEngine()
        {
            var car = new ElectricCar("Model 3", "Tesla", 2022, 45000m, UsageType.NewCar, 75, 200);

            Assert.That(car, Is.InstanceOf<Car>());
            Assert.That(car, Is.InstanceOf<IElectricEngine>());
            Assert.That(car.BatterySize, Is.EqualTo(75));
            Assert.That(car.MotorPower, Is.EqualTo(200));
        }

        [Test]
        public void HybridCar_InheritsFromPetrolCar()
        {
            var car = new HybridCar("Prius", "Toyota", 2021, 30000m, UsageType.NewCar, tankSize: 43, enginePower: 98, batterySize: 30, motorPower: 80, HybridType.FullHybrid);

            Assert.That(car, Is.InstanceOf<Car>());
            Assert.That(car, Is.InstanceOf<PetrolCar>());
            Assert.That(car, Is.InstanceOf<HybridCar>());
        }

        [Test]
        public void HybridCar_ImplementsElectricEngine()
        {
            var car = new HybridCar("Prius", "Toyota", 2021, 30000m, UsageType.NewCar, tankSize: 43, enginePower: 98, batterySize: 30, motorPower: 80, HybridType.PlugInHybrid);

            Assert.That(car, Is.InstanceOf<IElectricEngine>());
            Assert.That(car.BatterySize, Is.EqualTo(30));
            Assert.That(car.MotorPower, Is.EqualTo(80));
        }
        
        [Test]
        public void Extent_IsMaintainedSeparately_ForEachSubclass()
        {
            
            var petrol = new PetrolCar("A", "B", 2020, 10000m, UsageType.NewCar, 40, 100);
            var diesel = new DieselCar("C", "D", 2019, 12000m, UsageType.UsedCar, 50, 110);
            var electric = new ElectricCar("E", "F", 2022, 35000m, UsageType.NewCar, 70, 180);
            var hybrid = new HybridCar("G", "H", 2021, 28000m, UsageType.NewCar, 42, 95, 25, 75, HybridType.FullHybrid);

            Assert.That(PetrolCar.Extent.Collection, Does.Contain(petrol));
            Assert.That(DieselCar.Extent.Collection, Does.Contain(diesel));
            Assert.That(ElectricCar.Extent.Collection, Does.Contain(electric));
            Assert.That(HybridCar.Extent.Collection, Does.Contain(hybrid));
        }


        [Test]
        public void HybridCar_HasBothPetrolAndElectricProperties()
        {
            var car = new HybridCar("Prius", "Toyota", 2021, 30000m, UsageType.NewCar, tankSize: 43, enginePower: 98, batterySize: 30, motorPower: 80, HybridType.MildHybrid);

            Assert.That(car.TankSize, Is.EqualTo(43));
            Assert.That(car.EnginePower, Is.EqualTo(98));
            Assert.That(car.BatterySize, Is.EqualTo(30));
            Assert.That(car.MotorPower, Is.EqualTo(80));
        }

        [Test]
        public void CarBaseProperties_AreInheritedCorrectly()
        {
            var car = new ElectricCar("Leaf", "Nissan", 2020, 22000m, UsageType.UsedCar, 40, 110);

            Assert.That(car.Model, Is.EqualTo("Leaf"));
            Assert.That(car.Make, Is.EqualTo("Nissan"));
            Assert.That(car.Year, Is.EqualTo(2020));
            Assert.That(car.Price, Is.EqualTo(22000m));
        }
    }
}
