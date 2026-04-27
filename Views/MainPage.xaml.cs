using OfficeReservations.Helpers;

namespace OfficeReservations.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnReservationClicked(object? sender, EventArgs e)
    {
        var page = ServiceHelper.GetService<ServiceSelectionPage>();
        await Navigation.PushAsync(page);
    }

    private async void OnJoinQueueClicked(object? sender, EventArgs e)
    {
        var page = ServiceHelper.GetService<QueueJoinPage>();
        await Navigation.PushAsync(page);
    }
}