using CarDealership.Domain;

namespace CarDealership.Tests;

[TestFixture]
public class EmploymentTests
{
    [Test]
    public void Constructor_AddsEmploymentToAssociations()
    {
        var dealership = new Dealership("Main", "Downtown");
        var salesman = new Salesman("Sam Sales", "123456789");
        var start = DateTime.Today.AddYears(-1);

        var employment = new Employment(dealership, salesman, start, 50000m);

        Assert.That(salesman.Employments, Has.Member(employment), "Employee should have the employment registered.");
        Assert.That(dealership.Employments, Has.Member(employment), "Dealership should have the employment registered.");
        Assert.That(employment.IsActive, Is.True, "New employment with no end date should be active.");
    }

    [Test]
    public void Constructor_EndDateBeforeStart_Throws()
    {
        var dealership = new Dealership("Main", "Downtown");
        var salesman = new Salesman("Sam Sales", "123456789");
        var start = DateTime.Today;
        var end = start.AddDays(-1);

        Assert.That(() => new Employment(dealership, salesman, start, 30000m, end),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void StartDate_InFuture_Throws()
    {
        var dealership = new Dealership("Main", "Downtown");
        var salesman = new Salesman("Sam Sales", "123456789");
        var future = DateTime.Today.AddDays(1);

        Assert.That(() => new Employment(dealership, salesman, future, 30000m),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void Salary_Negative_Throws()
    {
        var dealership = new Dealership("Main", "Downtown");
        var salesman = new Salesman("Sam Sales", "123456789");
        var start = DateTime.Today.AddYears(-1);

        Assert.That(() => new Employment(dealership, salesman, start, -1m),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void TerminateEmployment_SetsEndDateAndMakesInactive_ReflectedInAssociations()
    {
        var dealership = new Dealership("Main", "Downtown");
        var salesman = new Salesman("Sam Sales", "123456789");
        var start = DateTime.Today.AddYears(-1);

        var employment = new Employment(dealership, salesman, start, 45000m);

        var endDate = DateTime.Today;
        employment.TerminateEmployment(endDate);

        Assert.That(employment.EndDate, Is.EqualTo(endDate));
        Assert.That(employment.IsActive, Is.False, "Employment terminated on or before today should be inactive.");

        // the same instance referenced in the associations should reflect the change
        var fromEmployee = salesman.Employments.First(e => ReferenceEquals(e, employment));
        var fromDealership = dealership.Employments.First(e => ReferenceEquals(e, employment));

        Assert.That(fromEmployee.EndDate, Is.EqualTo(endDate));
        Assert.That(fromDealership.EndDate, Is.EqualTo(endDate));
        Assert.That(fromEmployee.IsActive, Is.False);
        Assert.That(fromDealership.IsActive, Is.False);
    }

    [Test]
    public void UpdatingSalary_IsVisibleThroughAssociations()
    {
        var dealership = new Dealership("Branch", "Uptown");
        var mechanic = new Mechanic("Mick Mechanic", "987654321", "mick@example.com", "ASE");
        var start = DateTime.Today.AddYears(-2);

        var employment = new Employment(dealership, mechanic, start, 36000m);

        employment.Salary = 42000m;

        var associated = mechanic.Employments.First(e => ReferenceEquals(e, employment));
        Assert.That(associated.Salary, Is.EqualTo(42000m));
    }

    [Test]
    public void Delete_RemovesEmploymentFromAssociations()
    {
        var dealership = new Dealership("Sub", "East");
        var manager = new Manager("Mary Manager", "555555555", "mary@example.com", "Operations");
        var start = DateTime.Today.AddYears(-1);

        var employment = new Employment(dealership, manager, start, 70000m);

        Assert.That(manager.Employments, Has.Member(employment));
        Assert.That(dealership.Employments, Has.Member(employment));

        employment.ClearReferences();

        Assert.That(manager.Employments, Does.Not.Contain(employment), "Employment should be removed from employee after delete.");
        Assert.That(dealership.Employments, Does.Not.Contain(employment), "Employment should be removed from dealership after delete.");
    }
}