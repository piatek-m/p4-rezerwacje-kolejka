using System.ComponentModel;
using OfficeReservations.Services;
using OfficeReservations.Models;

namespace OfficeReservations.ViewModels;

public class ServiceSelectionViewModel : INotifyPropertyChanged
{
    private readonly DataService _dataService;

    public List<Department> Departments { get; private set; } = [];
    public Service? SelectedService { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    public ServiceSelectionViewModel(DataService dataService)
    {
        _dataService = dataService;
        Departments = _dataService.LoadDepartments();
    }
}