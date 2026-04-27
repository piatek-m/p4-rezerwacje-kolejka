using OfficeReservations.Models;
using OfficeReservations.Services;
using System.Windows.Input;

namespace OfficeReservations.ViewModels;

public class QueueJoinViewModel : BaseViewModel
{
    private readonly QueueService _queueService;

    private string _code = string.Empty;
    public string Code
    {
        get => _code;
        set
        {
            SetProperty(ref _code, value);
            ((Command)JoinCommand).ChangeCanExecute();
        }
    }

    private string _message = string.Empty;
    public string Message
    {
        get => _message;
        set => SetProperty(ref _message, value);
    }

    public Color MessageColor => Success ? Colors.Green : Colors.Red;

    private bool _hasMessage = false;
    public bool HasMessage
    {
        get => _hasMessage;
        set => SetProperty(ref _hasMessage, value);
    }

    private bool _success = false;
    public bool Success
    {
        get => _success;
        set
        {
            SetProperty(ref _success, value);
            OnPropertyChanged(nameof(MessageColor));
        }
    }

    public ICommand JoinCommand { get; }

    public QueueJoinViewModel(QueueService queueService)
    {
        _queueService = queueService;
        JoinCommand = new Command(OnJoin, () => Code.Length == 5);
    }

    private void OnJoin()
    {
        var (entry, message) = _queueService.JoinWithCode(Code.ToUpper());
        Message = message;
        HasMessage = true;
        Success = entry is not null;
    }
}