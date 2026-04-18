using System.Reflection;

namespace OfficeReservations.Services;

public static class FormReflectionHelper
{
    public static List<(PropertyInfo Property, string Label, string Placeholder)> GetFormFields<T>() => throw new NotImplementedException();
}