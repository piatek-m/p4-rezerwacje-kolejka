namespace OfficeReservations.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class DisplayLabelAttribute : Attribute
{
    public string Label { get; }
    public DisplayLabelAttribute(string label) => Label = label;
}