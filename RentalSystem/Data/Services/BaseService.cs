using System.ComponentModel.DataAnnotations;

namespace RentalSystem.Data.Services
{
    public abstract class BaseService
    {
        protected bool IsEntityStateValid(object model)
        {
            var validationContext = new ValidationContext(model);
            var validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(model, validationContext, validationResults,
                validateAllProperties: true);
        }
    }
}
