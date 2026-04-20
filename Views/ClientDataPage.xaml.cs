using OfficeReservations.ViewModels;

namespace OfficeReservations.Views;

public partial class ClientDataPage : ContentPage
{
    private ClientDataViewModel _viewModel;
    public ClientDataPage(ClientDataViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
        GenerateFormFields();
    }
    private void GenerateFormFields()
    {
        foreach (var (property, label, placeholder) in _viewModel.FormFields)
        {
            FormContainer.Children.Add(new Label
            {
                Text = label,
                FontSize = 14,
                FontAttributes = FontAttributes.Bold
            });

            var entry = new Entry
            {
                Placeholder = placeholder,
                Text = property.GetValue(_viewModel.ClientData)?.ToString() ?? string.Empty
            };

            entry.TextChanged += (s, e) => property.SetValue(_viewModel.ClientData, e.NewTextValue);

            FormContainer.Children.Add(entry);

            var errorLabel = new Label
            {
                TextColor = Colors.Red,
                FontSize = 12,
                IsVisible = false
            };

            // Binding validation errors
            _viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(ClientDataViewModel.ValidationErrors))
                {
                    if (_viewModel.ValidationErrors.TryGetValue(property.Name, out var error))
                    {
                        errorLabel.Text = error;
                        errorLabel.IsVisible = true;
                    }
                    else
                    {
                        errorLabel.IsVisible = false;
                    }
                }
            };

            FormContainer.Children.Add(errorLabel);
        }
    }
}