using Microsoft.AspNetCore.Mvc.ModelBinding;
using PolymorphicModelBinder.Entries;

namespace PolymorphicModelBinder.Providers
{
    internal interface IPolymorphicBindableModelBinderProvider
    {
        ICollection<PolymorphicBindableModelBinder> Provide(ModelBinderProviderContext context, IPolymorphicBindableCollection collection);
    }
}