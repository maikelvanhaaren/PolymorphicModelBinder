using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PolymorphicModelBinder.Entries;

internal class DefaultPolymorphicEntry<TAbstract> : IPolymorphicEntry
{
    private readonly ICollection<IPolymorphicImplementation> _typeEntries;
        
    public DefaultPolymorphicEntry()
    {
        _typeEntries = new List<IPolymorphicImplementation>();
    }
        
    public bool IsMatch(ModelBinderProviderContext context)
    {
        return context.Metadata.ModelType == typeof(TAbstract);
    }

    public void AddEntryType(IPolymorphicImplementation implementation) => _typeEntries.Add(implementation);

    public ICollection<IPolymorphicImplementation> GetTypes() => _typeEntries.ToList();
}