using System.Windows.Input;

namespace OfficeReservations.ViewModels;

public interface INavigableViewModel
{
    ICommand ProceedCommand { get; }
}