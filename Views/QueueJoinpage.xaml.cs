using OfficeReservations.ViewModels;

namespace OfficeReservations.Views;

public partial class QueueJoinPage : ContentPage
{
    public QueueJoinPage(QueueJoinViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}