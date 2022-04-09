using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PolymorphicModelBinder.Entries;

public interface IPolymorphicImplementation
{
    Type BindToType { get; }

    bool IsBindable(ModelBindingContext bindingContext);
}