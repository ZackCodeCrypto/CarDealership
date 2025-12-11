using NUnit.Framework;
using CarDealership.Domain;
using System.Linq;

namespace CarDealership.Tests
{
    public class EmployeeManagesTests
    {
        [Test]
        public void Manager_AddsManagedEmployee_SetsReverseConnection()
        {
            var m = new Manager("Alice", "703678123", "a@a.com", "Sales");
            var e = new Salesman("Bob", "703890456", 0.5m, "bob@bob.com");

            m.AddManagedEmployee(e);

            Assert.That(e.Manager, Is.EqualTo(m));
            Assert.That(m.ManagedEmployees, Does.Contain(e));
        }

        [Test]
        public void Employee_AssignManager_UpdatesBothSides()
        {
            var m = new Manager("Alice", "571898123", "a@a.com", "Sales");
            var e = new Mechanic("Carl", "571891789", "c@c.com", "Brakes");

            e.AssignManager(m);

            Assert.That(e.Manager, Is.EqualTo(m));
            Assert.That(m.ManagedEmployees.Count, Is.EqualTo(1));
            Assert.That(m.ManagedEmployees[0], Is.EqualTo(e));
        }

        [Test]
        public void Employee_ChangingManager_UpdatesBothManagers()
        {
            var m1 = new Manager("Alice", "571829123", "a@a.com", "Sales");
            var m2 = new Manager("Diana", "703456999", "d@d.com", "HR");

            var e = new Salesman("Bob", "571897456", 0.2m ,"b@b.com");

            e.AssignManager(m1);
            e.AssignManager(m2);

            Assert.That(e.Manager, Is.EqualTo(m2));
            Assert.That(m1.ManagedEmployees, Does.Not.Contain(e));
            Assert.That(m2.ManagedEmployees, Does.Contain(e));
        }

        [Test]
        public void RemoveManagedEmployee_RemovesReverseConnection()
        {
            var m = new Manager("Alice", "571678123", "a@a.com", "Sales");
            var e = new Mechanic("Carl", "571890789", "c@c.com", "Brakes");

            m.AddManagedEmployee(e);
            m.RemoveManagedEmployee(e);

            Assert.That(e.Manager, Is.Null);
            Assert.That(m.ManagedEmployees, Does.Not.Contain(e));
        }

        [Test]
        public void Manager_AddingSelf_Throws()
        {
            var m = new Manager("Alice", "571678923", "a@a.com", "Sales");

            Assert.Throws<InvalidOperationException>(() => m.AddManagedEmployee(m));
        }
    }
}
