using CarDealership.Repositories;

namespace CarDealership.Domain
{
    public class Employment
    {
        private DateTime _startDate;
        private DateTime? _endDate;
        private decimal _salary;

        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                if (value > DateTime.Today)
                    throw new ArgumentOutOfRangeException(nameof(value), "Start date cannot be in the future.");
                _startDate = value;
            }
        }
        public DateTime? EndDate
        {
            get => _endDate;
            set
            {
                if (value != null && value < StartDate)
                    throw new ArgumentOutOfRangeException(nameof(value), "End date cannot be before start date.");
                _endDate = value;
            }
        }
        public decimal Salary
        {
            get => _salary;
            set => _salary = value < 0
                ? throw new ArgumentOutOfRangeException(nameof(value), "Salary cannot be negative.")
                : value;
        }

        public bool IsActive => EndDate == null || EndDate > DateTime.Today;

        public Dealership Dealership { get; }

        public Employee Employee { get; }

        public Employment(Dealership dealership, Employee employee, DateTime startDate, decimal salary, DateTime? endDate = null)
        {
            StartDate = startDate;
            Salary = salary;
            EndDate = endDate;
            Dealership = dealership ?? throw new ArgumentNullException(nameof(dealership));
            Employee = employee ?? throw new ArgumentNullException(nameof(employee));

            employee.AddEmployment(this);
            dealership.AddEmployment(this);
        }

        public void TerminateEmployment(DateTime endDate)
        {
            if (endDate < StartDate)
                throw new ArgumentOutOfRangeException(nameof(endDate), "End date cannot be before start date.");
            EndDate = endDate;
        }

        public void ClearReferences()
        {
            Employee.RemoveEmployment(this);
            Dealership.RemoveEmployment(this);
        }
    }
}
