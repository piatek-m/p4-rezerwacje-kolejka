using OfficeReservations.Models;

namespace OfficeReservations.Services;

public class ReservationService
{
    private readonly DataService _dataService;

    public ReservationService(DataService dataService)
    {
        _dataService = dataService;
    }

    public List<TimeOnly> GetAvailableSlots(DateTime day) => throw new NotImplementedException();
    public string CreateReservation(string serviceId, string departmentId, DateTime slot, ClientData client) => throw new NotImplementedException();
    public bool CancelReservation(string code) => throw new NotImplementedException();
    public Reservation? GetReservation(string code) => throw new NotImplementedException();
    public ReservationStatus CheckExpiry(Reservation reservation) => throw new NotImplementedException();
}