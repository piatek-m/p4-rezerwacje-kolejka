using OfficeReservations.Models;

namespace OfficeReservations.Services;

public class DataService
{
    private readonly string _dataPath;

    public DataService(string dataPath)
    {
        _dataPath = dataPath;
    }

    public List<Reservation> LoadReservations() => throw new NotImplementedException();
    public void SaveReservations(List<Reservation> reservations) => throw new NotImplementedException();
    public List<QueueEntry> LoadQueue(string departmentId) => throw new NotImplementedException();
    public void SaveQueue(string departmentId, List<QueueEntry> queue) => throw new NotImplementedException();
    public List<Department> LoadDepartments() => throw new NotImplementedException();
    public WorkingHours LoadWorkingHours() => throw new NotImplementedException();
}