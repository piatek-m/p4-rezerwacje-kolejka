using Microsoft.Extensions.DependencyInjection;
using OfficeReservations.Helpers;

namespace OfficeReservations;

public partial class App : Application
{
	public App()
	{
		try
		{
			InitializeComponent();
		}
		catch (Exception ex)
		{
			File.WriteAllText("crash.log", ex.ToString());
			throw;
		}
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		try
		{
			return new Window(ServiceHelper.GetService<AppShell>());
		}
		catch (Exception ex)
		{
			File.WriteAllText("crash.log", ex.ToString());
			throw;
		}
	}
}