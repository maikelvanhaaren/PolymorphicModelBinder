using Microsoft.Extensions.Options;

namespace PolymorphicModelBinder
{
    internal class PolymorphicModelBinderOptionsValidator : IValidateOptions<PolymorphicModelBinderOptions>
    {
        public ValidateOptionsResult Validate(string name, PolymorphicModelBinderOptions options)
        {
            return ValidateOptionsResult.Success;
        }
    }
}