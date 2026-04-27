using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using OfficeReservations.Models;
using OfficeReservations.Services;

namespace OfficeReservations.ViewModels;

public class SummaryViewModel : BaseViewModel
{
    private readonly ReservationService _reservationService;

    private Service? _selectedService;
    public Service? SelectedService
    {
        get => _selectedService;
        set => SetProperty(ref _selectedService, value);
    }
    private DateTime? _selectedSlot;
    public DateTime? SelectedSlot
    {
        get => _selectedSlot;
        set => SetProperty(ref _selectedSlot, value);
    }

    private ClientData _clientData = new();
    public ClientData ClientData
    {
        get => _clientData;
        set
        {
            SetProperty(ref _clientData, value);
            OnPropertyChanged(nameof(EmailDisplay));
            OnPropertyChanged(nameof(PhoneDisplay));
        }
    }
    public string EmailDisplay => string.IsNullOrWhiteSpace(ClientData.Email) ? "Nie podano" : ClientData.Email;
    public string PhoneDisplay => string.IsNullOrWhiteSpace(ClientData.Phone) ? "Nie podano" : ClientData.Phone;

    private string _confirmationCode = string.Empty;
    public string ConfirmationCode
    {
        get => _confirmationCode;
        set => SetProperty(ref _confirmationCode, value);
    }

    private bool _isConfirmed = false;
    public bool IsConfirmed
    {
        get => _isConfirmed;
        set => SetProperty(ref _isConfirmed, value);
    }

    public ICommand ConfirmCommand { get; }

    public SummaryViewModel(ReservationService reservationService)
    {
        _reservationService = reservationService;
        ConfirmCommand = new Command(
            execute: OnConfirm,
            canExecute: () => !IsConfirmed
        );
    }

    private void OnConfirm()
    {
        try
        {
            if (SelectedService is null || SelectedSlot is null)
                return;

            ConfirmationCode = _reservationService.CreateReservation(
                SelectedService.Id ?? string.Empty,
                SelectedService.DepartmentId ?? string.Empty,
                SelectedSlot.Value,
                ClientData
            );

            IsConfirmed = true;
            ((Command)ConfirmCommand).ChangeCanExecute();
        }
        catch (Exception ex)
        {
            File.WriteAllText("crash_vm.log", ex.ToString());
            throw;
        }
    }
}