using OfficeReservations.Models;
using System.Windows.Input;
using OfficeReservations.Helpers;
using OfficeReservations.Views;
using System.Reflection;
using OfficeReservations.Validators;

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

    public List<(PropertyInfo Property, string Label, string Placeholder)> FormFields { get; }

    private Dictionary<string, string> _validationErrors = [];
    public Dictionary<string, string> ValidationErrors
    {
        get => _validationErrors;
        set => SetProperty(ref _validationErrors, value);
    }

    public ICommand ProceedCommand { get; }

    public ClientDataViewModel()
    {
        FormFields = FormReflectionHelper.GetFormFields<ClientData>();
        ProceedCommand = new Command(OnProceed);
    }

    private async void OnProceed()
    {
        var validator = new ClientDataValidator();
        var result = validator.Validate(ClientData);
        if (!result.IsValid)
        {
            ValidationErrors = result.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.First().ErrorMessage
                );
            return;
        }

        try
        {
            var summaryPage = ServiceHelper.GetService<SummaryPage>();
            if (summaryPage.BindingContext is SummaryViewModel vm)
            {
                vm.SelectedService = SelectedService;
                vm.SelectedSlot = SelectedSlot;
                vm.ClientData = ClientData;
            }
            await Shell.Current.Navigation.PushAsync(summaryPage);
        }
        catch (Exception ex)
        {
            File.WriteAllText("crash_vm.log", ex.ToString());
            throw;
        }
    }
}