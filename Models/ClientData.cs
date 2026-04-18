using OfficeReservations.Attributes;

namespace OfficeReservations.Models;

public class ClientData
{
    [DisplayLabel("Imię")]
    [Placeholder("Jan")]
    public string? FirstName { get; set; }

    [DisplayLabel("Nazwisko")]
    [Placeholder("Kowalski")]
    public string? LastName { get; set; }

    [DisplayLabel("PESEL")]
    [Placeholder("00000000000")]
    public string? Pesel { get; set; }

    [DisplayLabel("Email")]
    [Placeholder("jan@example.com")]
    public string? Email { get; set; }

    [DisplayLabel("Telefon")]
    [Placeholder("500000000")]
    public string? Phone { get; set; }
}
