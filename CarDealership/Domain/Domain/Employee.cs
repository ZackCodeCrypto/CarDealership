using CarDealership.Repositories;

namespace CarDealership.Domain;

public class Employee : Person
{
    private HashSet<string> _contactNumbers;
    private HashSet<Employment> _employments = [];
    
    private Manager? _manager;
    private Mechanic? _mechanic;
    private Salesman? _salesman;

    public IReadOnlyList<string> ContactNumbers => _contactNumbers.ToList().AsReadOnly();

    public IReadOnlyList<Employment> Employments => _employments.ToList().AsReadOnly();

    //Create manager
    public Employee(string name, string phoneNumber, Guid department, string? email = null): base(name, phoneNumber, email)
    {
        _manager = new Manager(this, department);
        _contactNumbers = [phoneNumber];
    }

    //Create mechanic
    public Employee(string name, string phoneNumber, string certification, string? email = null) : base(name, phoneNumber, email)
    {
        _mechanic = new Mechanic(this, certification);
        _contactNumbers = [phoneNumber];
    }

    //Create salesman
    public Employee(string name, string phoneNumber, decimal? commissionRate = null, string? email = null) : base(name, phoneNumber, email)
    {
        _salesman = new Salesman(this, commissionRate);
        _contactNumbers = [phoneNumber];
    }

    //Create manager and mechanic
    public Employee(string name, string phoneNumber, Guid department, string certification, string? email = null) : base(name, phoneNumber, email)
    {
        _manager = new Manager(this, department);
        _mechanic = new Mechanic(this, certification);
        _contactNumbers = [phoneNumber];
    }

    //Create mechanic and salesman
    public Employee(string name, string phoneNumber, string certification, decimal? commissionRate = null, string? email = null) : base(name, phoneNumber, email)
    {
        _mechanic = new Mechanic(this, certification);
        _salesman = new Salesman(this, commissionRate);
        _contactNumbers = [phoneNumber];
    }

    //Create manager and salesman
    public Employee(string name, string phoneNumber, Guid department, decimal? commissionRate = null, string? email = null) : base(name, phoneNumber, email)
    {
        _manager = new Manager(this, department);
        _salesman = new Salesman(this, commissionRate);
        _contactNumbers = [phoneNumber];
    }

    //Create manager, mechanic and salesman
    public Employee(string name, string phoneNumber, Guid department, string certification, decimal? commissionRate = null, string? email = null) : base(name, phoneNumber, email)
    {
        _manager = new Manager(this, department);
        _salesman = new Salesman(this, commissionRate);
        _mechanic = new Mechanic(this, certification);
        _contactNumbers = [phoneNumber];
    }

    public void AddContactNumber(string contactNumber)
    {
        if (IsPhoneNumberCorrect(contactNumber))
        {
            _contactNumbers.Add(contactNumber);
        }
    }

    public void RemoveContactNumber(string contactNumber)
    {
        if (_contactNumbers.Count == 1 && _contactNumbers.Contains(contactNumber))
        {
            throw new InvalidOperationException("Cannot remove the last contact number.");
        }
        _contactNumbers.Remove(contactNumber);
    }

    internal void AddEmployment(Employment employment)
    {
        ArgumentNullException.ThrowIfNull(employment);
        var existingActiveEmployment = _employments.FirstOrDefault(e => e.IsActive);

        if (existingActiveEmployment == null)
        {
            _employments.Add(employment);
            return;
        }

        if (existingActiveEmployment.Dealership != employment.Dealership)
        {
            throw new InvalidOperationException($"Employee {Name} is already employed at dealership {employment.Dealership.Name}.");
        }

        existingActiveEmployment.EndDate = DateTime.Now;
        _employments.Add(employment);
    }

    internal void RemoveEmployment(Employment employment)
    {
        ArgumentNullException.ThrowIfNull(employment);
        _employments.Remove(employment);
    }

    public bool ChangeToManager(Guid department)
    {
        if (_salesman != null)
        {
            return _salesman.ChangeToManager(department);
        }
        if (_mechanic != null)
        {
            return _mechanic.ChangeToManager(department);
        }
        throw new Exception("Employee is already a Manager");
    }

    public bool ChangeToSalesman(decimal? comissionRate)
    {
        if (_manager != null)
        {
            return _manager.ChangeToSalesman(comissionRate);
        }
        if (_mechanic != null)
        {
            return _mechanic.ChangeToSalesman(comissionRate);
        }
        throw new Exception("Employee is already a Salesman");
    }

    public bool ChangeToMechanic(string certification)
    {
        if (_manager != null)
        {
            return _manager.ChangeToMechanic(certification);
        }
        if (_salesman != null)
        {
            return _salesman.ChangeToMechanic(certification);
        }
        throw new Exception("Employee is already a Mechanic");
    }

    public bool ChangeToManagerMechanic(Guid department, string certification)
    {
        if (_manager != null)
        {
            return _manager.ChangeToManagerMechanic(certification);
        }
        if (_mechanic != null)
        {
            return _mechanic.ChangeToManagerMechanic(department);
        }
        return _salesman!.ChangeToManagerMechanic(department, certification);
    }

    public bool ChangeToSalesmanMechanic(string certification, decimal? comissionRate)
    {
        if (_manager != null)
        {
            return _manager.ChangeToSalesmanMechanic(certification, comissionRate);
        }
        if (_mechanic != null)
        {
            return _mechanic.ChangeToSalesmanMechanic(comissionRate);
        }
        return _salesman!.ChangeToSalesmanMechanic(certification);
    }

    public bool ChangeToManagerSalesman(Guid department, decimal? comissionRate)
    {
        if (_manager != null)
        {
            return _manager.ChangeToManagerSalesman(comissionRate);
        }
        if (_mechanic != null)
        {
            return _mechanic.ChangeToManagerSalesman(department, comissionRate);
        }
        return _salesman!.ChangeToManagerSalesman(department);
    }

    public bool ChangeToSalesmanMechanic(string certification)
    {
        if (_manager != null)
        {
            return _manager.ChangeToSalesmanMechanic(certification, null);
        }
        if (_mechanic != null)
        {
            return _mechanic.ChangeToSalesmanMechanic(null);
        }
        return _salesman!.ChangeToSalesmanMechanic(certification);
    }

    public bool ChangeToManagerSalesman(Guid department)
    {
        if (_manager != null)
        {
            return _manager.ChangeToManagerSalesman(null);
        }
        if (_mechanic != null)
        {
            return _mechanic.ChangeToManagerSalesman(department, null);
        }
        return _salesman!.ChangeToManagerSalesman(department);
    }

    public bool ChangeToManagerMechanicSalesman(string certification, Guid department)
    {
        if (_manager != null)
        {
            return _manager.ChangeToManagerMechanicSalesman(certification, null);
        }
        if (_mechanic != null)
        {
            return _mechanic.ChangeToManagerMechanicSalesman(department, null);
        }
        return _salesman!.ChangeToManagerMechanicSalesman(certification, department);
    }

    public bool ChangeToManagerMechanicSalesman(Guid department, decimal? comissionRate, string certificate)
    {
        if (_manager != null)
        {
            _manager.ChangeToManagerMechanicSalesman(certificate, comissionRate);
        }
        if (_mechanic != null)
        {
            return _mechanic.ChangeToManagerMechanicSalesman(department, comissionRate);
        }
        return _salesman!.ChangeToManagerMechanicSalesman(certificate, department);
    }

    private class Mechanic
    {
        private string _certification;
        private Employee _employee;

        public string Certification
        {
            get => _certification;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Certification cannot be empty.");
                _certification = value;
            }
        }

        public static ExtentRepository<Mechanic> Extent = new();

        public Mechanic(Employee employee, string certification)
        {
            _employee = employee ?? throw new ArgumentNullException(nameof(employee));
            Certification = certification;

            Extent.Add(this);
        }

        public bool ChangeToManager(Guid department)
        {
            _employee._manager = new Manager(_employee, department);
            _employee._mechanic = null;
            _employee._salesman = null;
            Extent.Remove(this);
            return true;
        }

        public bool ChangeToSalesman(decimal? comissionRate)
        {
            _employee._salesman = new Salesman(_employee, comissionRate);
            _employee._manager = null;
            _employee._mechanic = null;
            Extent.Remove(this);
            return true;
        }

        public bool ChangeToManagerMechanic(Guid department)
        {
            _employee._manager = new Manager(_employee, department);
            _employee._salesman = null;
            return true;
        }

        public bool ChangeToSalesmanMechanic(decimal? comissionRate)
        {
            _employee._salesman = new Salesman(_employee, comissionRate);
            _employee._manager = null;
            return true;
        }

        public bool ChangeToManagerSalesman(Guid department, decimal? comissionRate)
        {
            _employee._manager = new Manager(_employee, department);
            _employee._salesman = new Salesman(_employee, comissionRate);
            _employee._mechanic = null;
            return true;
        }

        public bool ChangeToManagerMechanicSalesman(Guid department, decimal? comissionRate)
        {
            _employee._manager = new Manager(_employee, department);
            _employee._salesman = new Salesman(_employee, comissionRate);
            return true;
        }
    }

    private class Manager
    {
        private Employee _employee;

        public Guid Department { get; set; }

        public static ExtentRepository<Manager> Extent = new();

        public Manager(Employee employee, Guid department)
        {
            _employee = employee ?? throw new ArgumentNullException(nameof(employee));
            Department = department;

            Extent.Add(this);
        }
        
        public bool ChangeToMechanic(string certification)
        {
            _employee._mechanic = new Mechanic(_employee, certification);
            _employee._manager = null;
            _employee._salesman = null;
            Extent.Remove(this);
            return true;
        }

        public bool ChangeToSalesman(decimal? comissionRate)
        {
            _employee._salesman = new Salesman(_employee, comissionRate);
            _employee._manager = null;
            _employee._mechanic = null;
            Extent.Remove(this);
            return true;
        }

        public bool ChangeToManagerMechanic(string certification)
        {
            _employee._mechanic = new Mechanic(_employee, certification);
            _employee._salesman = null;
            return true;
        }

        public bool ChangeToManagerSalesman(decimal? comissionRate)
        {
            _employee._salesman = new Salesman(_employee, comissionRate);
            _employee._mechanic = null;
            return true;
        }

        public bool ChangeToSalesmanMechanic(string certification, decimal? comissionRate)
        {
            _employee._mechanic = new Mechanic(_employee, certification);
            _employee._salesman = new Salesman(_employee, comissionRate);
            _employee._manager = null;
            return true;
        }

        public bool ChangeToManagerMechanicSalesman(string certification, decimal? comissionRate)
        {
            _employee._mechanic = new Mechanic(_employee, certification);
            _employee._salesman = new Salesman(_employee, comissionRate);
            return true;
        }
    }

    private class Salesman
    {
        private decimal _commissionRate;
        private Employee _employee;

        public decimal CommissionRate
        {
            get => _commissionRate;
            set => _commissionRate = value < 0
                ? throw new ArgumentOutOfRangeException(nameof(value), "Commission rate cannot be negative.")
                : value;
        }

        // Class/static attribute for our default commission rate
        public static decimal DefaultCommissionRate { get; set; } = 0.05m;

        public static ExtentRepository<Salesman> Extent = new();

        internal Salesman(Employee employee, decimal? commissionRate = null)
        {
            _employee = employee ?? throw new ArgumentNullException(nameof(employee));
            CommissionRate = commissionRate ?? DefaultCommissionRate;

            Extent.Add(this);
        }

        public bool ChangeToMechanic(string certification)
        {
            _employee._mechanic = new Mechanic(_employee, certification);
            _employee._manager = null;
            _employee._salesman = null;
            Extent.Remove(this);
            return true;
        }

        public bool ChangeToManager(Guid department)
        {
            _employee._manager = new Manager(_employee, department);
            _employee._mechanic = null;
            _employee._salesman = null;
            Extent.Remove(this);
            return true;
        }

        public bool ChangeToSalesmanMechanic(string certification)
        {
            _employee._mechanic = new Mechanic(_employee, certification);
            _employee._manager = null;
            return true;
        }

        public bool ChangeToManagerSalesman(Guid department)
        {
            _employee._manager = new Manager(_employee, department);
            _employee._mechanic = null;
            return true;
        }

        public bool ChangeToManagerMechanic(Guid department, string certification)
        {
            _employee._manager = new Manager(_employee, department);
            _employee._mechanic = new Mechanic(_employee, certification);
            _employee._salesman = null;
            return true;
        }

        public bool ChangeToManagerMechanicSalesman(string certification, Guid department)
        {
            _employee._manager = new Manager(_employee, department);
            _employee._mechanic = new Mechanic(_employee, certification);
            return true;
        }
    }
}
