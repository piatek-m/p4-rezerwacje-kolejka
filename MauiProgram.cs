using Microsoft.Extensions.Logging;
using OfficeReservations.Services;
using OfficeReservations.ViewModels;
using OfficeReservations.Views;

namespace OfficeReservations;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		// AppShell
		builder.Services.AddSingleton<AppShell>();

		// Services
		builder.Services.AddSingleton<DataService>();
		builder.Services.AddSingleton<ReservationService>();
		builder.Services.AddSingleton<QueueService>();

		// ViewModels
		builder.Services.AddTransient<ServiceSelectionViewModel>();
		builder.Services.AddTransient<CalendarViewModel>();
		builder.Services.AddTransient<ClientDataViewModel>();
		builder.Services.AddTransient<SummaryViewModel>();
		builder.Services.AddTransient<CancelReservationViewModel>();
		builder.Services.AddTransient<QueueJoinViewModel>();
		builder.Services.AddTransient<QueueStatusViewModel>();

		// Views
		builder.Services.AddTransient<ServiceSelectionPage>();
		builder.Services.AddTransient<CalendarPage>();
		builder.Services.AddTransient<ClientDataPage>();
		builder.Services.AddTransient<SummaryPage>();
		builder.Services.AddTransient<CancelReservationPage>();
		builder.Services.AddTransient<QueueJoinPage>();
		builder.Services.AddTransient<QueueStatusPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
