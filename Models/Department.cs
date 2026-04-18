namespace OfficeReservations.Models;

public class Department
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? OfficeName { get; set; }
    public List<Service> Services { get; set; } = [];
}