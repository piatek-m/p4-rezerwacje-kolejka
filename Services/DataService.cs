using System.Text.Json;
using OfficeReservations.Models;

namespace OfficeReservations.Services;

public class DataService
{
    private readonly string _dataPath;

    public DataService(string dataPath)
    {
        _dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
    }

    public List<Reservation> LoadReservations() => throw new NotImplementedException();
    public void SaveReservations(List<Reservation> reservations) => throw new NotImplementedException();
    public List<QueueEntry> LoadQueue(string departmentId) => throw new NotImplementedException();
    public void SaveQueue(string departmentId, List<QueueEntry> queue) => throw new NotImplementedException();
    public List<Department> LoadDepartments()
    {
        var json = File.ReadAllText(Path.Combine(_dataPath, "services.json"));
        return JsonSerializer.Deserialize<List<Department>>(json) ?? [];
    }
    public WorkingHours LoadWorkingHours() => throw new NotImplementedException();
    public Dictionary<DayOfWeek, WorkingHours?> LoadSchedule()
    {
        var json = File.ReadAllText(Path.Combine(_dataPath, "schedule.json"));
        var raw = JsonSerializer.Deserialize<Dictionary<string, WorkingHours?>>(json);

        if (raw is null)
            return [];

        return raw.ToDictionary(
            KeyValuePair => Enum.Parse<DayOfWeek>(KeyValuePair.Key),
            KeyValuePair => KeyValuePair.Value
        );

    }
}