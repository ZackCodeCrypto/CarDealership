namespace CarDealership.Domain;

public abstract class Employee : Person
{
    private HashSet<string> _contactNumbers;
    private HashSet<Employment> _employments = [];

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
