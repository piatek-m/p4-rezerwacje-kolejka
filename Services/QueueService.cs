using OfficeReservations.Models;

namespace OfficeReservations.Services;

public class QueueService
{
    private readonly DataService _dataService;
    private readonly ReservationService _reservationService;

    public Action<string>? OnReservationExpired { get; set; }

    public QueueService(DataService dataService, ReservationService reservationService)
    {
        _dataService = dataService;
        _reservationService = reservationService;
    }

    public QueueEntry JoinQueue(string departmentId, string? reservationCode)
      => throw new NotImplementedException();

    public int GetPosition(string code, string departmentId)
        => throw new NotImplementedException();

    public QueueEntry? CallNextClient(string departmentId)
        => throw new NotImplementedException();
}