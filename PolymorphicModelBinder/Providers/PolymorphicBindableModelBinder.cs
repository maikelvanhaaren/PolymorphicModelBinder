using Microsoft.AspNetCore.Mvc.ModelBinding;
using PolymorphicModelBinder.Entries;

namespace PolymorphicModelBinder.Providers
{
    internal class PolymorphicBindableModelBinder
    {
        public IPolymorphicBindable PolymorphicBindable { get; }
        public ModelMetadata ModelMetadata { get; }
        public IModelBinder ModelBinder { get; }

        public PolymorphicBindableModelBinder(IPolymorphicBindable polymorphicBindable, ModelMetadata modelMetadata, IModelBinder modelBinder)
        {
            PolymorphicBindable = polymorphicBindable;
            ModelMetadata = modelMetadata;
            ModelBinder = modelBinder;
        }

        public bool IsBindable(ModelBindingContext bindingContext) => PolymorphicBindable.IsBindable(bindingContext);
    }
}