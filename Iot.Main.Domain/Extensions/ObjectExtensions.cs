using System.ComponentModel.DataAnnotations;

namespace Iot.Main.Domain.Extensions
{
    public static class ObjectExtensions
    {
        public static void ValidateAndThrow(this object obj)
        {
            var context = new ValidationContext(obj);
            var results = new List<ValidationResult>();

            if(!Validator.TryValidateObject(obj, context, results, true))
            {
                var errors = results.ToDictionary(x => x.MemberNames.First(), x => x.ErrorMessage);
                throw new Shared.Exceptions.ValidationException(errors);
            }
        }
    }
}