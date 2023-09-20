using FluentValidation;
using SunTech.Customer.FuncApp.Requests;

namespace SunTech.Customer.FuncApp.Validators;

internal class CustomerUpdateRequestValidator : AbstractValidator<CustomerUpdateRequest>
{
    public CustomerUpdateRequestValidator()
    {
        RuleFor(customer => customer.FirstName)
            .NotNull()
            .NotEmpty()
            .WithMessage("Please specify a valid first name.");

        RuleFor(customer => customer.LastName)
            .NotNull()
            .NotEmpty()
            .WithMessage("Please specify a valid last name.");

        RuleFor(x => x.Email)
            .NotEmpty()
                .WithMessage("Email Required!")
            .EmailAddress()
                .WithMessage("Invalid Email!");

        RuleFor(customer => customer.Birthday)
            .NotNull()
            .NotEmpty().WithMessage("Please specify a birthday.");

        RuleFor(customer => customer.PartitionKey)
            .NotNull()
            .NotEmpty()
            .WithMessage("Please specify a partition key.");
    }
}
