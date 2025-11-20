using CarDealership.Domain;
using CarDealership.Enums;

namespace CarDealership.Tests;

[TestFixture]
public class CarTests
{
    [SetUp]
    public void SetUp()
    {
        PetrolCar.Extent.SaveExtent();
        PetrolCar.Extent.LoadExtent();
    }

    [TearDown]
    public void TearDown()
    {
        PetrolCar.Extent.DeleteExtent();
    }

    [Test]
    public void Constructor_WithEmptyModel_Throws()
    {
        Assert.That(
            () => new PetrolCar("", "Toyota", 2020, 20000m, UsageType.NewCar, 60, 140),
            Throws.ArgumentException);
    }

    [Test]
    public void Price_Negative_Throws()
    {
        var car = new PetrolCar("Corolla", "Toyota", 2020, 20000m, UsageType.NewCar, 60, 140);

        Assert.That(
            () => car.Price = -1m,
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void Year_InFuture_Throws()
    {
        var futureYear = DateTime.Today.Year + 1;

        Assert.That(
            () => new PetrolCar("Corolla", "Toyota", futureYear, 20000m, UsageType.NewCar, 60, 140),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void Mileage_SumsServiceRecords()
    {
        var car = new PetrolCar("Corolla", "Toyota", 2020, 20000m, UsageType.NewCar, 60, 140);
        car.AddServiceRecord(new ServiceRecord(DateTime.Today.AddDays(-10), "Test 1", "Check", 100));
        car.AddServiceRecord(new ServiceRecord(DateTime.Today.AddDays(-5), "Test 2", "Check", 50));

        Assert.That(car.Mileage, Is.EqualTo(150));
    }

    [Test]
    public void Extent_SaveAndLoad_KeepsCount()
    {
        var car = new PetrolCar("A", "B", 2020, 10000m, UsageType.NewCar, 60, 140);

        PetrolCar.Extent.SaveExtent();

        var originalCount = PetrolCar.Extent.Collection.Count;

        PetrolCar.Extent.LoadExtent();

        Assert.That(PetrolCar.Extent.Collection.Count, Is.EqualTo(originalCount));
    }
}