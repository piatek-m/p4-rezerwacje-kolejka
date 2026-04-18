using FluentValidation;
using OfficeReservations.Models;

namespace OfficeReservations.Validators;

public class ClientDataValidator : AbstractValidator<ClientData>
{
    public ClientDataValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("Imię jest wymagane");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Nazwisko jest wymagane");

        RuleFor(x => x.Pesel)
            .NotEmpty()
            .Length(11)
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
