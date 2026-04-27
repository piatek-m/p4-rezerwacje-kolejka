using OfficeReservations.Models;
using OfficeReservations.ViewModels;

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

    public (QueueEntry? Entry, string Message) JoinWithCode(string code)
    {
        var reservation = _reservationService.GetReservation(code);

        if (reservation is null)
            return (null, "Nie znaleziono rezerwacji o podanym kodzie.");

        if (reservation.Status == ReservationStatus.Cancelled)
            return (null, "Ta rezerwacja została anulowana.");

        if (reservation.Status == ReservationStatus.Completed)
            return (null, "Ta rezerwacja została już obsłużona.");

        var now = DateTime.Now;
        var slot = reservation.SlotDateTime;
        var minutesToSlot = (slot - now).TotalMinutes;

        if (minutesToSlot > 10)
            return (null, $"Twoja rezerwacja rozpocznie się o {slot:HH:mm}. " +
                          $"Wróć około {slot.AddMinutes(-10):HH:mm}.");

        if (now > slot.AddMinutes(2))
        {
            _reservationService.ExpireReservation(code);
            OnReservationExpired?.Invoke(code);
            return (null, $"Twoja rezerwacja na {slot:HH:mm} przepadła. " +
                          "Minął czas oczekiwania.");
        }

        var entry = new QueueEntry(
            code: code,
            departmentId: reservation.DepartmentId ?? string.Empty,
            hasReservation: true
        );

        var queue = _dataService.LoadQueue(entry.DepartmentId ?? string.Empty);
        queue.Add(entry);
        _dataService.SaveQueue(entry.DepartmentId ?? string.Empty, queue);

        var position = GetPosition(code, entry.DepartmentId ?? string.Empty);
        return (entry, $"Dołączono do kolejki. Twoja pozycja: {position}.");
    }

    public int GetPosition(string code, string departmentId)
    {
        var queue = _dataService.LoadQueue(departmentId)
            .Where(e => e.Status == QueueEntryStatus.Waiting)
            .ToList();

        // Priorytetowi pierwsi
        var ordered = queue
            .OrderByDescending(e => e.HasReservation)
            .ThenBy(e => e.JoinedAt)
            .ToList();

        var index = ordered.FindIndex(e => e.Code == code);
        return index == -1 ? -1 : index + 1;
    }
}