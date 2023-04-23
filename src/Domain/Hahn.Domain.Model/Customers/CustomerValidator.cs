using FluentValidation;
using Hahn.Domain.Model.Customers;

public class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        RuleFor(customer => customer.FirstName)
            .NotEmpty().WithMessage("'{PropertyName}' is required.")
            .MaximumLength(50).WithMessage("'{PropertyName}' must not exceed {MaxLength} characters.");

        RuleFor(customer => customer.LastName)
            .NotEmpty().WithMessage("'{PropertyName}' is required.")
            .MaximumLength(50).WithMessage("'{PropertyName}' must not exceed {MaxLength} characters.");

        RuleFor(customer => customer.DateOfBirth)
           .NotEmpty().WithMessage("'{PropertyName}' is required.")
           .LessThan(DateTime.Now.AddYears(-18)).WithMessage("'{PropertyName}' must be at least 18 years old.")
           .Must(BeInThePast).WithMessage("'{PropertyName}' must be in the past.");

        RuleFor(customer => customer.Email)
            .NotEmpty().WithMessage("'{PropertyName}' is required.")
            .EmailAddress().WithMessage("'{PropertyName}' is not a valid email address.")
            .MaximumLength(100).WithMessage("'{PropertyName}' must not exceed {MaxLength} characters.");

        RuleFor(customer => customer.PhoneNumber)
            .NotEmpty().WithMessage("'{PropertyName}' is required.")
            .Matches(@"^\+(?:[0-9]●?){6,14}[0-9]$").WithMessage("'{PropertyName}' is not a valid phone number.")
            .MaximumLength(15).WithMessage("'{PropertyName}' must not exceed {MaxLength} characters.");

        RuleFor(customer => customer.BankAccountNumber)
            .NotEmpty().WithMessage("'{PropertyName}' is required.")
            .MinimumLength(6).WithMessage("'{PropertyName}' must be at least {MinLength} characters long.")
            .MaximumLength(15).WithMessage("'{PropertyName}' must not exceed {MaxLength} characters.")
            .Matches("^[0-9]+$").WithMessage("'{PropertyName}' must contain only digits.");

    }
    bool BeInThePast(DateTime? dateOfBirth)
    {
        return dateOfBirth <= DateTime.Now.Date;
    }
}