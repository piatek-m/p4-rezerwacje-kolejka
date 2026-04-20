using System.Windows.Input;
using OfficeReservations.Helpers;
using OfficeReservations.Services;
using OfficeReservations.Models;
using OfficeReservations.Views;

namespace OfficeReservations.ViewModels;

public class CalendarViewModel : BaseViewModel, INavigableViewModel
{
    private readonly ReservationService _reservationService;
    public Service? SelectedService { get; set; }

    private DateTime _selectedDate = DateTime.Today;
    public DateTime SelectedDate
    {
        get => _selectedDate;
        set
        {
            if (SetProperty(ref _selectedDate, value))
                LoadAvailableSlots();
        }
    }

    private List<TimeOnly> _availableSlots = [];
    public List<TimeOnly> AvailableSlots
    {
        get => _availableSlots;
        private set
        {
            SetProperty(ref _availableSlots, value);
        }
    }
    public bool NoSlotsAvailable => AvailableSlots.Count == 0;

    private TimeOnly? _selectedSlot;
    public TimeOnly? SelectedSlot
    {
        get => _selectedSlot;
        set
        {
            SetProperty(ref _selectedSlot, value);
            ((Command)ProceedCommand).ChangeCanExecute();
        }
    }

    public ICommand ProceedCommand { get; }

    public CalendarViewModel(ReservationService reservationService)
    {
        _reservationService = reservationService;
        ProceedCommand = new Command(OnProceed);
        LoadAvailableSlots();
    }

    private void LoadAvailableSlots()
    {
        AvailableSlots = _reservationService.GetAvailableSlots(_selectedDate);
    }

    private async void OnProceed()
    {
        var clientDataPage = ServiceHelper.GetService<ClientDataPage>();
        if (clientDataPage.BindingContext is ClientDataViewModel vm)
        {
            vm.SelectedService = SelectedService;
            vm.SelectedSlot = new DateTime(
                _selectedDate.Year,
                _selectedDate.Month,
                _selectedDate.Day,
                _selectedSlot!.Value.Hour,
                _selectedSlot!.Value.Minute,
                0);
        }
        await Shell.Current.Navigation.PushAsync(clientDataPage);
    }


}