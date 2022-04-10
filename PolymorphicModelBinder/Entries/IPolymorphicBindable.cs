using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PolymorphicModelBinder.Entries
{
    public interface IPolymorphicBindable
    {
        Type BindToType { get; }

        bool IsBindable(ModelBindingContext bindingContext);
    }
}