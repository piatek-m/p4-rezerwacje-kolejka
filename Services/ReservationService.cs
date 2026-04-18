using OfficeReservations.Models;

namespace OfficeReservations.Services;

public class ReservationService
{
    private readonly DataService _dataService;

    public ReservationService(DataService dataService)
    {
        _dataService = dataService;
    }

    public List<TimeOnly> GetAvailableSlots(DateTime day)
    {
        if (day.Date < DateTime.Today)
            return [];

        // Is it open today?
        var schedule = _dataService.LoadSchedule();
        if (!schedule.TryGetValue(day.DayOfWeek, out var hours) || hours is null)
            return [];

        var allSlots = GenerateSlots(hours.Open, hours.Close, day);

        var reservations = _dataService.LoadReservations();
        var takenSlots = reservations
            .Where(r => r.SlotDateTime.Date == day.Date && r.Status == ReservationStatus.Active)
            .Select(r => TimeOnly.FromDateTime(r.SlotDateTime))
            .ToHashSet(); // Contains: O(1) instead of Linked List's O(n)

        return allSlots
            .Where(slot => !takenSlots.Contains(slot))
            .ToList();
    }

    private List<TimeOnly> GenerateSlots(TimeOnly openingHour, TimeOnly closingHour, DateTime day)
    {
        var slots = new List<TimeOnly>();
        var slotTime = openingHour;

        // Only slots whose starting time hasn't yet passed 
        if (day.Date == DateTime.Today)
        {
            var now = TimeOnly.FromDateTime(DateTime.Now);
            while (slotTime <= now)
                slotTime = slotTime.AddMinutes(10);
        }

        while (slotTime < closingHour)
        {
            slots.Add(slotTime);
            slotTime = slotTime.AddMinutes(10);
        }

        return slots;
    }

    public string CreateReservation(string serviceId, string departmentId, DateTime slot, ClientData client) => throw new NotImplementedException();
    public bool CancelReservation(string code) => throw new NotImplementedException();
    public Reservation? GetReservation(string code) => throw new NotImplementedException();
    public ReservationStatus CheckExpiry(Reservation reservation) => throw new NotImplementedException();
}