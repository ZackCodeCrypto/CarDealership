namespace CarDealership.Domain;

public abstract class Employee : Person, IManageable
{
    private HashSet<string> _contactNumbers;
    private HashSet<Employment> _employments = [];
    
    private Manager? _manager;
    public Manager? Manager => _manager;

    public IReadOnlyList<string> ContactNumbers => _contactNumbers.ToList().AsReadOnly();

    public IReadOnlyList<Employment> Employments => _employments.ToList().AsReadOnly();

    protected Employee(
        string name,
        string phoneNumber,
        string? email = null)
        : base(name, phoneNumber, email)
    {
        _contactNumbers = [phoneNumber];
    }

    public void AddContactNumber(string contactNumber)
    {
        if (IsPhoneNumberCorrect(contactNumber))
        {
            _contactNumbers.Add(contactNumber);
        }
    }
    
    public void AssignManager(Manager manager)
    {
        ArgumentNullException.ThrowIfNull(manager);

        if (ReferenceEquals(this, manager))
            throw new InvalidOperationException("An employee cannot manage themselves.");

        if (_manager == manager)
            return;
        
        _manager?.RemoveManagedEmployee(this, updateEmployeeSide: false);

        _manager = manager;

        manager.AddManagedEmployee(this, updateEmployeeSide: false);
    }
    
    public void RemoveManager()
    {
        if (_manager == null)
            return;

        var oldManager = _manager;
        _manager = null;
        
        oldManager.RemoveManagedEmployee(this, updateEmployeeSide: false);
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
}
