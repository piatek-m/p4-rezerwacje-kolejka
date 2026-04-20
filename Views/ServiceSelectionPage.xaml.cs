using OfficeReservations.ViewModels;
using OfficeReservations.Services;
using OfficeReservations.Helpers;
using OfficeReservations.Models;

namespace OfficeReservations.Views;

public partial class ServiceSelectionPage : ContentPage
{
    public ServiceSelectionPage()
    {

        try
        {
            InitializeComponent();
            BindingContext = ServiceHelper.GetService<ServiceSelectionViewModel>();
        }
        catch (Exception ex)
        {
            File.WriteAllText("crash_page.log", ex.ToString());
            throw;
        }
    }
}