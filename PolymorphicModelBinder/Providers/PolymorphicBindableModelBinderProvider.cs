using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PolymorphicModelBinder.Entries;

namespace PolymorphicModelBinder.Providers
{
    internal class PolymorphicBindableModelBinderProvider : IPolymorphicBindableModelBinderProvider
    {
        public ICollection<PolymorphicBindableModelBinder> Provide(ModelBinderProviderContext context, IPolymorphicBindableCollection collection)
        {
            var binders = new List<PolymorphicBindableModelBinder>();
        
            foreach (var entryType in collection.GetTypes())
            {
                var modelMetadata = context.MetadataProvider.GetMetadataForType(entryType.BindToType);
                var binder = context.CreateBinder(modelMetadata);
                binders.Add(new PolymorphicBindableModelBinder(entryType, modelMetadata, binder));
            }

            return binders;
        }
    }
}