﻿using PolymorphicModelBinder.Builders;
using PolymorphicModelBinder.Entries;

namespace PolymorphicModelBinder;

public class PolymorphicModelBinderOptions
{
    internal ICollection<IPolymorphicEntry> Entries { get; } = new List<IPolymorphicEntry>();
    
    public void Add<TAbstract>(Action<IBindPolymorphicBuilder<TAbstract>> binder)
    {
        var builder = new BindPolymorphicBuilder<TAbstract>();
        binder.Invoke(builder);
        var entry = builder.Build();
        Entries.Add(entry);
    }
}