using CarDealership.Domain;

namespace CarDealership.Tests
{
    [TestFixture]
    public class FinancingPlanSaleTests
    {
        [Test]
        public void AssigningPlanToSale_WithPlanAssignMethod_SetsBothSidesAndIsIdempotent()
        {
            var customer = new Customer("Alice", "111222333", "DL-A1", "Needs compact");
            var salesman = new Employee("Sam", "444555666");
            var sale = new Sale(DateTime.Today, "Card", customer, salesman);

            var plan = new FinancingPlan(300m, 3.5);

            plan.AssignToSale(sale);

            Assert.That(sale.FinancingPlan, Is.SameAs(plan), "Sale should reference the assigned financing plan.");
            Assert.That(plan.Sales, Has.Member(sale), "FinancingPlan should contain the sale in its collection.");
            Assert.That(plan.Sales.Count(s => ReferenceEquals(s, sale)), Is.EqualTo(1), "Sale should appear only once in plan's collection.");

            Assert.DoesNotThrow(() => plan.AssignToSale(sale));
            Assert.That(plan.Sales.Count(s => ReferenceEquals(s, sale)), Is.EqualTo(1));
        }

        [Test]
        public void AssigningSecondPlanToSameSale_ThrowsInvalidOperation()
        {
            var customer = new Customer("Bob", "777888999", "DL-B2", "Needs SUV");
            var salesman = new Employee("Nina", "000111222");
            var sale = new Sale(DateTime.Today, "Cash", customer, salesman);

            var plan1 = new FinancingPlan(250m, 2.9);
            var plan2 = new FinancingPlan(350m, 4.1);

            plan1.AssignToSale(sale);

            Assert.Throws<InvalidOperationException>(() => plan2.AssignToSale(sale));
        }

        [Test]
        public void RemovingPlan_FromSale_ClearsBothSides()
        {
            var customer = new Customer("Carl", "101010101", "DL-C3", "Needs sedan");
            var salesman = new Employee("Alex", "202020202");
            var sale = new Sale(DateTime.Today, "Card", customer, salesman);

            var plan = new FinancingPlan(150m, 1.9);

            plan.AssignToSale(sale);

            sale.RemoveFinancingPlan();

            Assert.That(sale.FinancingPlan, Is.Null, "Sale's FinancingPlan should be null after removal.");
            Assert.That(plan.Sales, Does.Not.Contain(sale), "FinancingPlan should no longer reference the sale after removal.");

            plan.AssignToSale(sale);
            plan.RemoveSale(sale);

            Assert.That(sale.FinancingPlan, Is.Null, "Sale's FinancingPlan should be null after plan.RemoveSale.");
            Assert.That(plan.Sales, Does.Not.Contain(sale), "Plan should not contain the sale after RemoveSale.");
        }

        [Test]
        public void SalesCollectionAndSaleReference_AreSameInstanceReferences()
        {
            var customer = new Customer("Diane", "303030303", "DL-D4", "Needs hatchback");
            var salesman = new Employee("Zed", "404040404");
            var sale = new Sale(DateTime.Today, "Transfer", customer, salesman);

            var plan = new FinancingPlan(99m, 0.5);

            plan.AssignToSale(sale);

            var referenced = plan.Sales.First();
            Assert.That(ReferenceEquals(referenced, sale), Is.True);
            Assert.That(sale.FinancingPlan, Is.SameAs(plan));
        }

        [Test]
        public void RemoveFinancingPlan_WhenPlanAssignedViaPlanSide_ClearsBothSides()
        {
            var customer = new Customer("Frank", "707070707", "DL-F6", "Needs van");
            var salesman = new Employee("Olga", "808080808");
            var sale = new Sale(DateTime.Today, "Cash", customer, salesman);

            var plan = new FinancingPlan(180m, 3.0);

            plan.AssignToSale(sale);

            Assert.That(sale.FinancingPlan, Is.SameAs(plan));
            Assert.That(plan.Sales, Has.Member(sale));

            sale.RemoveFinancingPlan();

            Assert.That(sale.FinancingPlan, Is.Null);
            Assert.That(plan.Sales, Does.Not.Contain(sale));
        }

        [Test]
        public void RemoveFinancingPlan_WhenNoPlanAssigned_IsNoOp()
        {
            var customer = new Customer("Gina", "909090909", "DL-G7", "Needs coupe");
            var salesman = new Employee("Hank", "101010101");
            var sale = new Sale(DateTime.Today, "Card", customer, salesman);

            Assert.DoesNotThrow(() => sale.RemoveFinancingPlan());
            Assert.That(sale.FinancingPlan, Is.Null);
        }
    }
}
