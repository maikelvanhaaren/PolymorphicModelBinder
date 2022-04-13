using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PolymorphicModelBinder.Providers;

namespace PolymorphicModelBinder
{
    internal class PolymorphicModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            var polymorphicModelBinderOptions = context.Services.GetRequiredService<IOptions<PolymorphicModelBinderOptions>>().Value;

            var bindableCollection = polymorphicModelBinderOptions.Entries
                .FirstOrDefault(entry => entry.IsMatch(context));
        
            if (bindableCollection == null)
                return null;

            var bindableModelBindersProvider = context.Services.GetRequiredService<IPolymorphicBindableModelBinderProvider>();
        
            var binders = bindableModelBindersProvider
                .Provide(context, bindableCollection);
        
            return new PolymorphicModelBinder(binders);
        }
    }
}