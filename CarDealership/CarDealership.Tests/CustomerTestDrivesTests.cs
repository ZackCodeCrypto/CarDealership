using NUnit.Framework;
using CarDealership.Domain;
using CarDealership.Enums;

namespace CarDealership.Tests;

[TestFixture]
public class CustomerTestDrivesTests
{
    private Car MakeCar(string vin = "VIN1")
    {
        return new PetrolCar("ModelX", "BrandY", 2020, 20000m, UsageType.NewCar, 45, 200);
    }

    [Test]
    public void AddTestDrive_AssociatesBothSides()
    {
        var c = new Customer("Prince", "571555720", "DL123", "Needs SUV");
        var car = MakeCar();
        
        var td = new TestDrive(DateTime.Today.AddDays(-1), 10, 5, c, car);

        Assert.That(c.TestDrives.Contains(td));
        Assert.That(td.Customer, Is.EqualTo(c));
    }

    [Test]
    public void RemoveTestDrive_UnlinksFromCustomer()
    {
        var c = new Customer("Bob", "571780123", "DL999", "Fast car");
        var car = MakeCar();
        var td = new TestDrive(DateTime.Today.AddDays(-1), 15, 5, c, car);

        td.Unlink();

        Assert.That(!c.TestDrives.Contains(td));
    }

    [Test]
    public void AddTestDrive_DoesNotDuplicate()
    {
        var c = new Customer("Billy", "703470789", "DL111", "Cheap car");
        var car = MakeCar();
        var td = new TestDrive(DateTime.Today.AddDays(-2), 20, 4, c, car);
        
        c.GetType()
         .GetMethod("AddTestDrive", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
         .Invoke(c, new object[] { td });

        Assert.That(c.TestDrives.Count, Is.EqualTo(1));
    }

    [Test]
    public void Constructor_Throws_WhenCustomerIsNull()
    {
        var car = MakeCar();

        Assert.Throws<ArgumentNullException>(() =>
            new TestDrive(DateTime.Today.AddDays(-1), 10, 5, null!, car));
    }

    [Test]
    public void ModifyingAssociation_ChangesOwningCustomer()
    {
        var c1 = new Customer("Andrew", "571879111", "DL1", "Something");
        var c2 = new Customer("Rhys", "703489222", "DL2", "Else");
        var car = MakeCar();
        
        var td = new TestDrive(DateTime.Today.AddDays(-1), 10, 5, c1, car);
        
        td.Unlink();
        typeof(Customer)
            .GetMethod("AddTestDrive", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.Invoke(c2, new object[] { td });

        Assert.That(!c1.TestDrives.Contains(td));
        Assert.That(c2.TestDrives.Contains(td));
    }
}
