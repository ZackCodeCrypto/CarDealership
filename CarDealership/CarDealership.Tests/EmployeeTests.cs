using CarDealership.Domain;

namespace CarDealership.Tests
{
    [TestFixture]
    public class EmployeeTests
    {
        [Test]
        public void Manager_CannotChangeToManager_Throws()
        {
            var dept = Guid.NewGuid();
            var employee = new Employee("Alice Manager", "571555720", dept, null);

            var ex = Assert.Throws<Exception>(() => employee.ChangeToManager(Guid.NewGuid()));
            Assert.That(ex!.Message, Is.EqualTo("Employee is already a Manager"));
        }

        [Test]
        public void Manager_CanChangeToSalesman_ThenBecomesSalesman()
        {
            var dept = Guid.NewGuid();
            var employee = new Employee("Alice Manager", "571555720", dept, null);

            var changed = employee.ChangeToSalesman(0.10m);
            Assert.That(changed);

            var ex = Assert.Throws<Exception>(() => employee.ChangeToSalesman(0.05m));
            Assert.That(ex!.Message, Is.EqualTo("Employee is already a Salesman"));
        }

        [Test]
        public void Salesman_NegativeCommission_ThrowsOnCreation()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _ = new Employee("Bad Sales", "571555720", -0.5m);
            });
        }

        [Test]
        public void Salesman_CanChangeToManager_ThenCannotChangeToManagerAgain()
        {
            var salesman = new Employee("Sam Sales", "571555720", 0.08m);

            var toManager = salesman.ChangeToManager(Guid.NewGuid());
            Assert.That(toManager);

            var ex = Assert.Throws<Exception>(() => salesman.ChangeToManager(Guid.NewGuid()));
            Assert.That(ex!.Message, Is.EqualTo("Employee is already a Manager"));
        }

        [Test]
        public void Mechanic_CanChangeToSalesman_AndMechanicChangeThrowsWhenAlreadyMechanic()
        {
            var mechanic = new Employee("Mike Mech", "571555720", "Cert-A", null);

            var ex = Assert.Throws<Exception>(() => mechanic.ChangeToMechanic("NewCert"));
            Assert.That(ex!.Message, Is.EqualTo("Employee is already a Mechanic"));

            var toSales = mechanic.ChangeToSalesman(0.07m);
            Assert.That(toSales);
        }

        [Test]
        public void MechanicAndSalesman_Composite_CanChangeToManager()
        {
            var combo = new Employee("Combo", "571555720", "Cert-X", 0.06m);

            // Salesman part should handle ChangeToManager first (salesman != null in ChangeToManager)
            var toManager = combo.ChangeToManager(Guid.NewGuid());
            Assert.That(toManager);

            var ex = Assert.Throws<Exception>(() => combo.ChangeToManager(Guid.NewGuid()));
            Assert.That(ex!.Message, Is.EqualTo("Employee is already a Manager"));
        }

        [Test]
        public void RemovingLastContactNumber_ThrowsInvalidOperationException()
        {
            var dept = Guid.NewGuid();
            var employee = new Employee("Contact Test", "571555720", dept, null);

            Assert.Throws<InvalidOperationException>(() => employee.RemoveContactNumber("571555720"));
        }
    }
}
