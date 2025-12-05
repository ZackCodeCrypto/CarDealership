using CarDealership.Domain;
using CarDealership.Enums;

namespace CarDealership.Tests;

[TestFixture]
public class PersonAndSaleTests
{
    [Test]
    public void OptionalEmail_AllowsNullButNotEmpty()
    {
        var customer = new Customer("Bob Ross", "571678534", "DL123", "Needs car");
        
        customer.Email = null;

        Assert.That(
            () => customer.Email = "",
            Throws.ArgumentException);
    }

    [Test]
    public void Customer_AddContactNumber_Empty_Throws()
    {
        var customer = new Customer("Bob Ross", "571678534", "DL123", "Needs car");

        Assert.That(
            () => customer.AddContactNumber(""),
            Throws.ArgumentException);
    }

    [Test]
    public void Sale_TotalPrice_ComputedFromParts()
    {
        var customer = new Customer("Bob Ross", "571678534", "DL1", "Needs car");
        var salesman = new Salesman("Nick Rochefort", "703457459", 40000m);
        var car = new PetrolCar("Model", "Make", 2020, 20000m, UsageType.NewCar, 60, 140);
        var accessory = new Accessory("GPS", "Electronics", 500m);
        var insurance = new InsurancePolicy("P1", "Provider", "Full", 800m);

        var sale = new Sale(
            DateTime.Today,
            "Card",
            customer,
            salesman,
            car,
            insurance,
            new[] { accessory });

        Assert.That(
            sale.TotalPrice,
            Is.EqualTo(20000m + 500m + 800m));
    }
}