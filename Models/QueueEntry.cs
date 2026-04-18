namespace OfficeReservations.Models;

public class QueueEntry
{
    public string? Code { get; set; }
    public string? DepartmentId { get; set; }
    public bool HasReservation { get; set; }
    public DateTime JoinedAt { get; set; }
    public QueueEntryStatus Status { get; set; }
}
