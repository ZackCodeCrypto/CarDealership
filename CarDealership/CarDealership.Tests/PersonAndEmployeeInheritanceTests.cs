using System;
using NUnit.Framework;
using CarDealership.Domain;

namespace CarDealership.Tests
{
    [TestFixture]
    public class PersonAndEmployeeInheritanceTests
    {
        // Person is abstract and cannot be instantiated
        [Test]
        public void Person_IsAbstract()
        {
            Assert.That(typeof(Person).IsAbstract, Is.True);
        }

        // Employee is abstract and inherits from Person
        [Test]
        public void Employee_IsAbstract_AndInheritsFromPerson()
        {
            Assert.That(typeof(Employee).IsAbstract, Is.True);
            Assert.That(typeof(Employee).IsSubclassOf(typeof(Person)), Is.True);
        }

        // Salesman inherits from Employee
        [Test]
        public void Salesman_InheritsFromEmployee()
        {
            var salesman = new Salesman("Nick Rochefort", "703890456", 0.5m, "scuffed@gmail.com");

            Assert.That(salesman, Is.InstanceOf<Employee>());
            Assert.That(salesman, Is.InstanceOf<Person>());
        }

        // Mechanic inherits from Employee
        [Test]
        public void Mechanic_InheritsFromEmployee()
        {
            var mechanic = new Mechanic("Joseph Belcher", "571891789", "c@c.com", "Brakes");

            Assert.That(mechanic, Is.InstanceOf<Employee>());
            Assert.That(mechanic, Is.InstanceOf<Person>());
        }

        // Manager inherits from Employee
        [Test]
        public void Manager_InheritsFromEmployee()
        {
            var manager = new Manager("Dylan Rock", "703678123", "a@a.com", "Sales");

            Assert.That(manager, Is.InstanceOf<Employee>());
            Assert.That(manager, Is.InstanceOf<Person>());
        }

        // Employee properties from Person are set correctly
        [Test]
        public void Employee_InheritsPersonProperties()
        {
            var manager = new Manager("Alice", "703678123", "alice@test.com", "Sales");

            Assert.That(manager.Name, Is.EqualTo("Alice"));
            Assert.That(manager.Email, Is.EqualTo("alice@test.com"));
        }

        // Disjoint inheritance: subclasses are not interchangeable
        [Test]
        public void Employee_Subclasses_AreDisjoint()
        {
            var mechanic = new Mechanic("Carl", "571891789", "c@c.com", "Brakes");

            Assert.That(mechanic, Is.Not.InstanceOf<Salesman>());
            Assert.That(mechanic, Is.Not.InstanceOf<Manager>());
        }
    }
}
