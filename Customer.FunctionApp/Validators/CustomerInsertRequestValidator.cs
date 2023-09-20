using FluentValidation;
using SunTech.Customer.FuncApp.Requests;

namespace SunTech.Customer.FuncApp.Validators;
public class CustomerInsertRequestValidator : AbstractValidator<CustomerInsertRequest>
{
    public CustomerInsertRequestValidator()
    {
        RuleFor(customer => customer.FirstName).NotNull().NotEmpty().WithMessage("Please specify a valid first name.");
        RuleFor(customer => customer.LastName).NotNull().NotEmpty().WithMessage("Please specify a valid last name.");
        RuleFor(customer => customer.Email).NotNull().NotEmpty().EmailAddress().WithMessage("Please specify a valid email.");
        RuleFor(customer => customer.Birthday).NotNull().NotEmpty().WithMessage("Please specify a birthday.");
    }
}
