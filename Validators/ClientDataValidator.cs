using FluentValidation;
using OfficeReservations.Models;

namespace OfficeReservations.Validators;

public class ClientDataValidator : AbstractValidator<ClientData>
{
    private const string NamePattern =
        @"^[a-zA-ZąćęłńóśźżĄĆĘŁŃÓŚŹŻáčďéěíňřšťúůýžÁČĎÉĚÍŇŘŠŤÚŮÝŽàâäéèêëîïôùûüÀÂÄÉÈÊËÎÏÔÙÛÜ\s\-']+$";

    public ClientDataValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("Imię jest wymagane")
            .Matches(NamePattern)
            .WithMessage("Imię może zawierać tylko litery łacińskie"); ;

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Nazwisko jest wymagane")
            .Matches(NamePattern)
            .WithMessage("Nazwisko może zawierać tylko litery łacińskie"); ;

        RuleFor(x => x.Pesel)
            .NotEmpty()
            .WithMessage("PESEL jest wymagany")
            .Length(11)
            .WithMessage("PESEL jest za krótki lub za długi")
            .Matches("^[0-9]{11}$")
            .WithMessage("PESEL musi składać się z 11 cyfr");

        // Phone or Email is required
        RuleFor(x => x.Email)
            .NotEmpty()
            .When(x => string.IsNullOrWhiteSpace(x.Phone))
            .WithMessage("Podaj email lub numer telefonu");

        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.Email))
            .WithMessage("Nieprawidłowy format email");
    }
}
