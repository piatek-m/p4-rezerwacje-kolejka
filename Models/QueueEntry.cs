namespace OfficeReservations.Models;

public class QueueEntry
{
    public string? Code { get; set; }
    public string? DepartmentId { get; set; }
    public bool HasReservation { get; set; }
    public DateTime JoinedAt { get; set; }
    public QueueEntryStatus Status { get; set; }

    public QueueEntry() { }

    public QueueEntry(string code, string departmentId, bool hasReservation)
    {
        Code = code;
        DepartmentId = departmentId;
        HasReservation = hasReservation;
        JoinedAt = DateTime.Now;
        Status = QueueEntryStatus.Waiting;
    }
}