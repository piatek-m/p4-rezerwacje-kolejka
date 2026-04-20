using System.Windows.Input;
using OfficeReservations.Services;

namespace OfficeReservations.ViewModels;

public class CalendarViewModel : BaseViewModel, INavigableViewModel
{
    private readonly ReservationService _reservationService;

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

    private void OnProceed()
    {
        // go to next screen
    }

}