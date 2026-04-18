namespace OfficeReservations.Models;

public class Reservation
{
    public string? Code { get; set; }
    public string? ServiceId { get; set; }
    public string? DepartmentId { get; set; }
    public DateTime SlotDateTime { get; set; }
    public ReservationStatus Status { get; set; }
    public ClientData Client { get; set; } = new();
    public DateTime CreatedAt { get; set; }
}
