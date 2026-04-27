using System.Text.Json;
using OfficeReservations.Models;

namespace OfficeReservations.Services;

public class DataService
{
    public DataService() { }

    private string ReservationsPath =>
    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "reservations.json");

    public List<Reservation> LoadReservations()
    {
        File.WriteAllText("load_path.log", ReservationsPath);

        if (!File.Exists(ReservationsPath))
            return [];

        var json = File.ReadAllText(ReservationsPath);
        return JsonSerializer.Deserialize<List<Reservation>>(json) ?? [];
    }
    public void SaveReservations(List<Reservation> reservations)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(ReservationsPath)!);

        File.WriteAllText("save_path.log", ReservationsPath);
        var json = JsonSerializer.Serialize(reservations);
        File.WriteAllText(ReservationsPath, json);
    }
    public List<QueueEntry> LoadQueue(string departmentId) => throw new NotImplementedException();
    public void SaveQueue(string departmentId, List<QueueEntry> queue) => throw new NotImplementedException();
    public List<Department> LoadDepartments()
    {
        using var stream = FileSystem.OpenAppPackageFileAsync("department_services.json").GetAwaiter().GetResult();
        using var reader = new StreamReader(stream);
        var json = reader.ReadToEnd();
        return JsonSerializer.Deserialize<List<Department>>(json) ?? [];
    }
    public WorkingHours LoadWorkingHours() => throw new NotImplementedException();
    public Dictionary<DayOfWeek, WorkingHours?> LoadSchedule()
    {
        using var stream = FileSystem.OpenAppPackageFileAsync("schedule.json").GetAwaiter().GetResult();
        using var reader = new StreamReader(stream);
        var json = reader.ReadToEnd();
        var raw = JsonSerializer.Deserialize<Dictionary<string, WorkingHours?>>(json);

        if (raw is null)
            return [];

        return raw.ToDictionary(
            KeyValuePair => Enum.Parse<DayOfWeek>(KeyValuePair.Key),
            KeyValuePair => KeyValuePair.Value
        );

    }
}