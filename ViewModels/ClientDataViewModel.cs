using OfficeReservations.Models;
using System.Windows.Input;

namespace OfficeReservations.ViewModels;

public class ClientDataViewModel : BaseViewModel, INavigableViewModel
{
    public Service? SelectedService { get; set; }
    public DateTime? SelectedSlot { get; set; }

    private ClientData _clientData = new();
    public ClientData ClientData
    {
        get => _clientData;
        set => SetProperty(ref _clientData, value);
    }

    public ICommand ProceedCommand { get; }

    public ClientDataViewModel()
    {
        ProceedCommand = new Command(OnProceed, () => SelectedSlot.HasValue);
    }

    private async void OnProceed()
    {
        // nawigacja do SummaryPage
    }
}