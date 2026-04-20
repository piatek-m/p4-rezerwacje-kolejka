using System.ComponentModel;
using OfficeReservations.Services;
using OfficeReservations.Models;
using System.Windows.Input;

namespace OfficeReservations.ViewModels;

public class ServiceSelectionViewModel : BaseViewModel, INavigableViewModel
{
    private readonly DataService _dataService;

    public List<DepartmentGroup> Departments { get; private set; } = [];
    private Service? _selectedService;
    public Service? SelectedService
    {
        get => _selectedService;
        set => SetProperty(ref _selectedService, value);
    }

    public ICommand ProceedCommand { get; }

    public ServiceSelectionViewModel(DataService dataService)
    {
        _dataService = dataService;
        Departments = _dataService.LoadDepartments()
            .Select(d => new DepartmentGroup(d))
            .ToList();
        ProceedCommand = new Command(OnProceed, () => SelectedService is not null);
    }

    private void OnProceed()
    {
        // navigate to Calendar
    }
}