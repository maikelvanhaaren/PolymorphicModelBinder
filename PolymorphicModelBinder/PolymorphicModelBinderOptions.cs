using System;
using System.Collections.Generic;
using PolymorphicModelBinder.Builders;
using PolymorphicModelBinder.Entries;

namespace PolymorphicModelBinder
{
    public class PolymorphicModelBinderOptions
    {
        internal ICollection<IPolymorphicBindableCollection> Entries { get; } = new List<IPolymorphicBindableCollection>();
    
        public void Add<TAbstract>(Action<IBindPolymorphicBuilder<TAbstract>> binder)
        {
            var builder = new BindPolymorphicBuilder<TAbstract>();
            binder.Invoke(builder);
            var entry = builder.Build();
            Entries.Add(entry);
        }
    }
}