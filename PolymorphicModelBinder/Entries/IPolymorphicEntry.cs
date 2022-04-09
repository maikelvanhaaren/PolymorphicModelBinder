using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PolymorphicModelBinder.Entries;

internal interface IPolymorphicEntry
{
    bool IsMatch(ModelBinderProviderContext context);
    
    ICollection<IPolymorphicImplementation> GetTypes();
}