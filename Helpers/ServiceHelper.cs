namespace OfficeReservations.Helpers;

public static class ServiceHelper
{
    public static TService GetService<TService>()
        where TService : notnull
        => IPlatformApplication.Current!.Services.GetRequiredService<TService>();
}