using OfficeReservations.ViewModels;
using OfficeReservations.Services;

namespace OfficeReservations.Views;

public partial class ServiceSelectionPage : ContentPage
{
    public ServiceSelectionPage()
    {
        InitializeComponent();
        BindingContext = new ServiceSelectionViewModel(dataService);
    }
}