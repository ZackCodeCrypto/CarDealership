using CarDealership.Domain;
using CarDealership.Enums;

namespace CarDealership.Tests;

[TestFixture]
public class DealershipTests
{
    [Test]
    public void Name_Empty_Throws()
    {
        Assert.That(
            () => new Dealership("", "Tyson's Corner"),
            Throws.ArgumentException);
    }

    [Test]
    public void CarsAvailable_CountsOnlyAvailableCars()
    {
        var d = new Dealership("Koons", "Tyson's Corner");

        var car1 = new PetrolCar("A", "B", 2020, 10000m, UsageType.NewCar, 60, 140);
        var car2 = new PetrolCar("C", "D", 2020, 10000m, UsageType.NewCar, 60, 140);
        car2.MarkSold();

        d.AddCar(car1);
        d.AddCar(car2);

        Assert.That(d.CarsAvailable, Is.EqualTo(1));
    }
    
    [Test]
    public void AddCarByVin_AddsCar_AndSetsReverseReference()
    {
        var d = new Dealership("Koons", "Tyson's Corner");
        var car = new PetrolCar("A", "B", 2020, 10000m, UsageType.NewCar, 60, 140);

        d.AddCar(car);

        Assert.That(d.GetCarByVin(car.VIN), Is.EqualTo(car));
        Assert.That(car.Dealership, Is.EqualTo(d));
    }

    [Test]
    public void AddCarByVin_CannotAddSameCarToTwoDealerships()
    {
        var d1 = new Dealership("Koons", "Tyson's Corner");
        var d2 = new Dealership("Sheehy", "Fairfax");

        var car = new PetrolCar("A", "B", 2020, 10000m, UsageType.NewCar, 60, 140);

        d1.AddCar(car);

        Assert.That(() => d2.AddCar(car),
            Throws.InvalidOperationException);
    }

    [Test]
    public void RemoveCarByVin_RemovesAndClearsReverseReference()
    {
        var d = new Dealership("Koons", "Tyson's Corner");
        var car = new PetrolCar("A", "B", 2020, 10000m, UsageType.NewCar, 60, 120);

        d.AddCar(car);

        d.RemoveCarByVin(car.VIN);

        Assert.That(d.Cars.Count, Is.EqualTo(0));
        Assert.That(d.GetCarByVin(car.VIN), Is.Null);
        Assert.That(car.Dealership, Is.Null);
    }


    [Test]
    public void GetCarByVin_ReturnsNull_WhenVinNotPresent()
    {
        var d = new Dealership("Koons", "Tyson's Corner");

        Assert.That(d.GetCarByVin(Guid.NewGuid()), Is.Null);
    }

    [Test]
    public void AddCarByVin_Throws_WhenVinAlreadyExists()
    {
        var d = new Dealership("Koons", "Tyson's Corner");

        var car1 = new PetrolCar("A", "B", 2020, 10000m, UsageType.NewCar, 60, 140);
        var car2 = new PetrolCar("C", "D", 2021, 12000m, UsageType.NewCar, 55, 130);
        
        typeof(Car)
            .GetProperty("VIN")!
            .SetValue(car2, car1.VIN);

        d.AddCar(car1);

        Assert.That(() => d.AddCar(car2),
            Throws.InvalidOperationException);
    }
    
}