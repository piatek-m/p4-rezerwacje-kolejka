using OfficeReservations.Models;

namespace OfficeReservations.Services;

public class ReservationService
{
    private readonly DataService _dataService;

    public ReservationService(DataService dataService)
    {
        _dataService = dataService;
    }

    public List<TimeOnly> GetAvailableSlots(DateTime day, string departmentId)
    {
        if (day.Date < DateTime.Today)
            return [];

        // Is it open today?
        var schedule = _dataService.LoadSchedule();
        if (!schedule.TryGetValue(day.DayOfWeek, out var hours) || hours is null)
            return [];

        var allSlots = GenerateSlots(hours.Open, hours.Close, day);

        var reservations = _dataService.LoadReservations();

        File.WriteAllText("slots_debug.log",
             $"DepartmentId szukany: '{departmentId}'\n" +
             string.Join("\n", reservations.Select(r =>
                 $"Rezerwacja: dept='{r.DepartmentId}' data={r.SlotDateTime} status={r.Status}")));

        var takenSlots = reservations
            .Where(r => r.SlotDateTime.Date == day.Date
                     && r.Status == ReservationStatus.Active
                     && r.DepartmentId == departmentId)
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

    public string CreateReservation(string serviceId, string departmentId, DateTime slot, ClientData client)
    {
        var reservations = _dataService.LoadReservations();

        var code = CodeGenerator.GenerateUniqueCode(reservations.Select(r => r.Code));

        var reservation = new Reservation
        {
            Code = code,
            ServiceId = serviceId,
            DepartmentId = departmentId,
            SlotDateTime = slot,
            Status = ReservationStatus.Active,
            Client = client,
            CreatedAt = DateTime.Now
        };

        reservations.Add(reservation);
        _dataService.SaveReservations(reservations);

        return code;
    }
    public bool CancelReservation(string code) => throw new NotImplementedException();
    public Reservation? GetReservation(string code)
    {
        return _dataService.LoadReservations()
            .FirstOrDefault(r => r.Code == code);
    }

    public void ExpireReservation(string code)
    {
        var reservations = _dataService.LoadReservations();
        var reservation = reservations.FirstOrDefault(r => r.Code == code);
        if (reservation is not null)
        {
            reservation.Status = ReservationStatus.Expired;
            _dataService.SaveReservations(reservations);
        }
    }
}