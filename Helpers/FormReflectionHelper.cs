using System.Reflection;
using OfficeReservations.Attributes;

namespace OfficeReservations.Helpers;

public static class FormReflectionHelper
{
    public static List<(PropertyInfo Property, string Label, string Placeholder)> GetFormFields<T>()
    {
        return typeof(T)
            .GetProperties()
            .Select(prop => (
                Property: prop,
                Label: prop.GetCustomAttribute<DisplayLabelAttribute>()?.Label ?? prop.Name,
                Placeholder: prop.GetCustomAttribute<PlaceholderAttribute>()?.Text ?? string.Empty
            ))
            .Where(x => x.Label != x.Property.Name || x.Placeholder != string.Empty)
            .ToList();
    }
}