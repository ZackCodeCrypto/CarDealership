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
}