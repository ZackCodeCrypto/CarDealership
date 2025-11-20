using CarDealership.Domain;

namespace CarDealership.Tests;

[TestFixture]
public class CarTests
{
    [SetUp]
    public void SetUp()
    {
        Car.Extent.SaveExtent();
        Car.Extent.LoadExtent();
    }

    [TearDown]
    public void TearDown()
    {
        Car.Extent.DeleteExtent();
    }

    [Test]
    public void Constructor_WithEmptyModel_Throws()
    {
        Assert.That(
            () => new Car("", "Toyota", 2020, 20000m, UsageType.NewCar),
            Throws.ArgumentException);
    }

    [Test]
    public void Price_Negative_Throws()
    {
        var car = new Car("Corolla", "Toyota", 2020, 20000m, UsageType.NewCar);

        Assert.That(
            () => car.Price = -1m,
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void Year_InFuture_Throws()
    {
        var futureYear = DateTime.Today.Year + 1;

        Assert.That(
            () => new Car("Corolla", "Toyota", futureYear, 20000m, UsageType.NewCar),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void Mileage_SumsServiceRecords()
    {
        var car = new Car("Corolla", "Toyota", 2020, 20000m, UsageType.NewCar);
        car.AddServiceRecord(new ServiceRecord(DateTime.Today.AddDays(-10), "Test 1", "Check", 100));
        car.AddServiceRecord(new ServiceRecord(DateTime.Today.AddDays(-5), "Test 2", "Check", 50));

        Assert.That(car.Mileage, Is.EqualTo(150));
    }

    [Test]
    public void Extent_SaveAndLoad_KeepsCount()
    {
        var car = new Car("A", "B", 2020, 10000m, UsageType.NewCar);

        Car.Extent.SaveExtent();

        var originalCount = Car.Extent.Collection.Count;

        Car.Extent.LoadExtent();

        Assert.That(Car.Extent.Collection.Count, Is.EqualTo(originalCount));
    }
}