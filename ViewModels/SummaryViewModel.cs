using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using OfficeReservations.Models;

namespace OfficeReservations.ViewModels;

public class SummaryViewModel : BaseViewModel
{
    // public Reservation Reservation { get; }

    public Service? SelectedService { get; set; }
    public DateTime? SelectedSlot { get; set; }

    private ClientData _clientData = new();
    public ClientData ClientData
    {
        get => _clientData;
        set => SetProperty(ref _clientData, value);
    }

    public ICommand ConfirmCommand { get; }

    public SummaryViewModel()
    {
        ConfirmCommand = new Command(OnConfirm);
    }

    private void OnConfirm()
    {
        //potwierdzenie
    }
}