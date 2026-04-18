using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using OfficeReservations.Models;

namespace OfficeReservations.ViewModels;

public class SummaryViewModel : INotifyPropertyChanged
{
    public Reservation Reservation { get; }
    public ICommand ConfirmCommand { get; }

    public SummaryViewModel(Reservation reservation)
    {
        Reservation = reservation;
        ConfirmCommand = new Command(OnConfirm);
    }

    private void OnConfirm()
    {

    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}