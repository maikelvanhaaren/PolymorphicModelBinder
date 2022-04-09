using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PolymorphicModelBinder.Entries;

namespace PolymorphicModelBinder;

internal class PolymorphicModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        var options = context.Services.GetRequiredService<IOptions<PolymorphicModelBinderOptions>>().Value;

        var matchedEntry = options.Entries.FirstOrDefault(entry => entry.IsMatch(context));
        
        if (matchedEntry == null)
            return null;
        
        var binders = new List<(IPolymorphicImplementation, ModelMetadata, IModelBinder)>();
        
        foreach (var entryType in matchedEntry.GetTypes())
        {
            var modelMetadata = context.MetadataProvider.GetMetadataForType(entryType.BindToType);
            var binder = context.CreateBinder(modelMetadata);
            binders.Add((entryType, modelMetadata, binder));
        }
        
        return new PolymorphicModelBinder(binders);
    }
}