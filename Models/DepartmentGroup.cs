namespace OfficeReservations.Models;

public class DepartmentGroup : List<Service>
{
    public string Id { get; }
    public string Name { get; }
    public string OfficeName { get; }

    public DepartmentGroup(Department department) : base(department.Services)
    {
        Id = department.Id ?? string.Empty;
        Name = department.Name ?? string.Empty;
        OfficeName = department.OfficeName ?? string.Empty;
    }
}