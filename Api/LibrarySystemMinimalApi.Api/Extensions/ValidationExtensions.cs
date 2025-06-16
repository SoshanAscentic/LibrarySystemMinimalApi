using System.ComponentModel.DataAnnotations;

namespace LibrarySystemMinimalApi.Api.Extensions
{
    public static class ValidationExtensions
    {
        public static IResult? ValidateModel<T>(this T model) where T : class
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model);

            if (!Validator.TryValidateObject(model, validationContext, validationResults, true))
            {
                var errors = validationResults
                    .GroupBy(x => x.MemberNames.FirstOrDefault() ?? "")
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(x => x.ErrorMessage).ToArray()
                    );

                return Results.BadRequest(new { errors });
            }

            return null;
        }
        
    }
}
