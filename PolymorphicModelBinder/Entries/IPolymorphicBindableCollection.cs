using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PolymorphicModelBinder.Entries
{
    internal interface IPolymorphicBindableCollection
    {
        bool IsMatch(ModelBinderProviderContext context);
        ICollection<IPolymorphicBindable> GetTypes();
    }
}