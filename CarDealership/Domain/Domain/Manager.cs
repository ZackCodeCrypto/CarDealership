using CarDealership.Repositories;

namespace CarDealership.Domain;

public class Manager : Employee, IManageable
{
    private string _department;
    
    private HashSet<Employee> _managedEmployees = new();
    public IReadOnlyList<Employee> ManagedEmployees => _managedEmployees.ToList().AsReadOnly();

    public string Department
    {
        get => _department;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Department cannot be empty.");
            _department = value;
        }
    }

    public static ExtentRepository<Manager> Extent = new();

    public Manager(
        string name,
        string phoneNumber,
        string? email,
        string department)
        : base(name, phoneNumber, email)
    {
        Department = department;

        Extent.Add(this);
    }
    
    public void AddManagedEmployee(Employee employee, bool updateEmployeeSide = true)
    {
        ArgumentNullException.ThrowIfNull(employee);

        if (ReferenceEquals(employee, this))
            throw new InvalidOperationException("A manager cannot manage themselves.");

        if (_managedEmployees.Add(employee) && updateEmployeeSide)
        {
            employee.AssignManager(this);
        }
    }

    public void RemoveManagedEmployee(Employee employee, bool updateEmployeeSide = true)
    {
        ArgumentNullException.ThrowIfNull(employee);

        if (_managedEmployees.Remove(employee) && updateEmployeeSide)
        {
            employee.RemoveManager();
        }
    }
}