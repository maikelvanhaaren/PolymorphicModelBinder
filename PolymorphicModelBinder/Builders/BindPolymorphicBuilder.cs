using Microsoft.AspNetCore.Mvc.ModelBinding;
using PolymorphicModelBinder.Entries;
using PolymorphicModelBinder.Entries.Custom;
using PolymorphicModelBinder.Entries.TypeInValue;

namespace PolymorphicModelBinder.Builders;

internal class BindPolymorphicBuilder<TAbstract> : IBindPolymorphicBuilder<TAbstract>
{
    private readonly DefaultPolymorphicEntry<TAbstract> _entry;

    public BindPolymorphicBuilder()
    {
        _entry = new DefaultPolymorphicEntry<TAbstract>();
    }
    
    public IBindPolymorphicBuilder<TAbstract> AddFromTypeInValue<TImplementation>() where TImplementation : TAbstract, new()
    {
        return Add(new TypeInValuePolymorphicImplementation(typeof(TImplementation)));
    }

    public IBindPolymorphicBuilder<TAbstract> AddFromCustom<TImplementation>(Func<ModelBindingContext, bool> isMatchFunc) where TImplementation : TAbstract, new()
    {
        return Add(new CustomPolymorphicImplementation(typeof(TImplementation), isMatchFunc));
    }

    public IBindPolymorphicBuilder<TAbstract> Add(IPolymorphicImplementation implementation)
    {
        _entry.AddEntryType(implementation);
        return this;
    }

    public IPolymorphicEntry Build() => _entry;
}