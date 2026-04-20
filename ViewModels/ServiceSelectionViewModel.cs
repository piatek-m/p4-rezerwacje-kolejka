using System.ComponentModel;
using System.Linq;
using OfficeReservations.Services;
using OfficeReservations.Models;
using OfficeReservations.Helpers;
using OfficeReservations.Views;
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
        set
        {
            SetProperty(ref _selectedService, value);
            ((Command)ProceedCommand).ChangeCanExecute();
        }
    }

    public ICommand ProceedCommand { get; }

    public ServiceSelectionViewModel(DataService dataService)
    {
        _dataService = dataService;
        try
        {
            Departments = _dataService.LoadDepartments()
                .Select(d => new DepartmentGroup(d))
                .ToList();
        }
        catch (Exception ex)
        {
            File.WriteAllText("crash_vm.log", ex.ToString());
            throw;
        }
        ProceedCommand = new Command(OnProceed, () => SelectedService is not null);
    }

    private async void OnProceed()
    {
        try
        {
            var calendarPage = ServiceHelper.GetService<CalendarPage>();
            if (calendarPage.BindingContext is CalendarViewModel vm)
                vm.SelectedService = SelectedService!;

            await Shell.Current.Navigation.PushAsync(calendarPage);
        }
        catch (Exception ex)
        {
            File.WriteAllText("crash_vm.log", ex.ToString());
            throw;
        }
    }
}