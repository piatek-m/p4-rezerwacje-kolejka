using OfficeReservations.ViewModels;

namespace OfficeReservations.Views;

public partial class SummaryPage : ContentPage
{
    public SummaryPage(SummaryViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}