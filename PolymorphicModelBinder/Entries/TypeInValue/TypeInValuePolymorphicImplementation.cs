using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PolymorphicModelBinder.Entries.TypeInValue;

internal class TypeInValuePolymorphicImplementation : IPolymorphicImplementation
{
    public const string FieldName = "TypeInValuePolymorphic";

    public TypeInValuePolymorphicImplementation(Type type)
    {
        BindToType = type;
    }

    public Type BindToType { get; }

    public bool IsBindable(ModelBindingContext bindingContext)
    {
        var fieldName = ModelNames.CreatePropertyModelName(bindingContext.ModelName, FieldName);
        var value = bindingContext.ValueProvider.GetValue(fieldName);

        if (value == ValueProviderResult.None)
            throw new ArgumentNullException(nameof(value));

        return Type.GetType(value.FirstValue) == BindToType;
    }
}