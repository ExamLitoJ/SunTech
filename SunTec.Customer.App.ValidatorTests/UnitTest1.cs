using SunTech.Customer.FuncApp.Validators;

namespace SunTec.Customer.App.ValidatorTests;

public class CustomerInsertRequestValidatorTest
{
    [Theory]
    [InlineData(null, null, null)]
    [InlineData("", "", "")]
    [InlineData(" ", " ", " ")]
    public void FieldsEmpty_ReturnsValidationErrors(string name, string email, string message)
    {
        var validator = new CustomerInsertRequestValidator();

        validator.ShouldHaveValidationErrorFor(x => x.Name, name);
        validator.ShouldHaveValidationErrorFor(x => x.Email, email);
        validator.ShouldHaveValidationErrorFor(x => x.Message, message);
    }
}