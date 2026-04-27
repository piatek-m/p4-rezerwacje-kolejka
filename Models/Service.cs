namespace OfficeReservations.Models;

public class Service
{
    public string? Id { get; set; }
    public string? DepartmentId { get; set; }
    public string? DepartmentName { get; set; }
    public string? Name { get; set; }
    public int SlotDurationMinutes { get; set; } = 10;
}