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

        var car1 = new Car("A", "B", 2020, 10000m, UsageType.NewCar);
        var car2 = new Car("C", "D", 2020, 10000m, UsageType.NewCar);
        car2.MarkSold();

        d.AddCar(car1);
        d.AddCar(car2);

        Assert.That(d.CarsAvailable, Is.EqualTo(1));
    }
}